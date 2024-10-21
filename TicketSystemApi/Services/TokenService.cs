using Domain.DTO.User;
using Domain.Interface.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using TicketSystemApi.DB;
using TicketSystemApi.Settings;

namespace TicketSystemApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IConfiguration configuration)
        {
            _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        }

        public List<Claim> GetClaimsFromObject<T>(T obj)
        {
            var claims = new List<Claim>();

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                var value = property.GetValue(obj)?.ToString() ?? string.Empty;
                claims.Add(new Claim(property.Name, value));
            }

            return claims;
        }


        public string GenerateToken<T>(T body, int expirationInHours = 2)
        {
            var claims = GetClaimsFromObject(body);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(expirationInHours),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = credentials
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.Zero 
                };


                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public string RefreshToken(string token, int expirationInHours = 2)
        {
            var principal = ValidateToken(token);

            if (principal == null)
            {
                throw new SecurityTokenException("Token inválido");
            }

            var newToken = GenerateToken(principal.Claims, expirationInHours);
            return newToken;
        }

        public HttpAuthenticateUserResponse GetObjectFromToken(IEnumerable<Claim> userClaims)
        {
            try
            {
                var roleIdIdClaim = userClaims.FirstOrDefault(c => c.Type == "RoleId")?.Value;
                var departmentIdClaim = userClaims.FirstOrDefault(c => c.Type == "DepartmentId")?.Value;
                var userIdClaim = userClaims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                if (roleIdIdClaim == null || !int.TryParse(roleIdIdClaim, out int categoryId))
                {
                    throw new Exception("RoleId inválida o no encontrada en los claims.");
                }
                if (departmentIdClaim == null || !int.TryParse(departmentIdClaim, out int departmentId))
                {
                    throw new Exception("DepartmentIdClaim inválida o no encontrada en los claims.");
                }
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                {
                    throw new Exception("userIdClaim inválida o no encontrada en los claims.");
                }


                return new HttpAuthenticateUserResponse
                {
                    RoleId = categoryId ,
                    DepartmentId = departmentId,
                    UserId = userId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al extraer los claims del token", ex);
            }
        }

    }
}
