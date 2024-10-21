using Domain.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    public interface ITokenService
    {
        public  string GenerateToken <T>(T body, int expirationInHours = 2);
        public ClaimsPrincipal ValidateToken(string token);
        public string RefreshToken(string token, int expirationInHours = 2);

        public HttpAuthenticateUserResponse GetObjectFromToken(IEnumerable<Claim> userClaims);

    }
}
