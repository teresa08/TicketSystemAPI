using Domain.DTO;
using Domain.DTO.User;
using Domain.Interface.Repositories.User;
using Domain.Interface.Services;
using Domain.Interface.UseCase.User;
using Domain.Request.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaction.User
{
    public class UserCase : IUserCase
    {
        private IUserRepository _userContext;
        private ITokenService _tokenService;
        public UserCase(IUserRepository userContext, ITokenService tokenService)
        {
            _userContext = userContext;
            _tokenService = tokenService;
        }

        public async Task<MessagePayload<HttpGetUserResponse>> AuthenticateUser(httpAuthenticateUserRequest user)
        {
            try
            {
                var _user = await _userContext.AuthenticateUser(user);
                if (_user != null)
                {
                    return new MessagePayload<HttpGetUserResponse>
                    {
                      Payload = new HttpGetUserResponse
                      {
                             Name = _user.Name,
                             Email = _user.Email,
                             Role = _user.Role,
                             Department = _user.Department,
                      },
                        Status = 200,
                        Token = _tokenService.GenerateToken<HttpAuthenticateUserResponse>(_user),
                        Response = EResponse.Success
                    };
                }
                return new MessagePayload<HttpGetUserResponse>
                {
                    ErrorCode = "Credenciales invalidas",
                    Status = 404,
                    Response = EResponse.Error
                };
     
            }
            catch (Exception ex)
            {

                return new MessagePayload<HttpGetUserResponse>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<string>> CreateUser(CreateUserRequest user)
        {
            try
            {
                if (await _userContext.UserExist(user.Email))
                {
                    return new MessagePayload<string>
                    {
                        ErrorCode = "Este correo ya está en uso",
                        Status = 500,
                        Response = EResponse.Error
                    };
                }
                await _userContext.CreateUser(user);

                return new MessagePayload<string>
                {
                    Payload = "Usuario Creado",
                    Status = 200,
                    Response = EResponse.Success
                };
            }
            catch (Exception ex)
            {

                return new MessagePayload<string>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<int>> DeleteUser(int id)
        {
            try
            {
                return new MessagePayload<int>
                {
                    Status = 200,
                    Payload = await _userContext.DeleteUser(id),
                    Response = EResponse.Success
                };
            }
            catch (Exception ex)
            {

                return new MessagePayload<int>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<HttpGetUserResponse>> GetUser(int id)
        {
            try
            {
                return new MessagePayload<HttpGetUserResponse>
                {
                    Status = 200,
                    Payload = await _userContext.GetUser(id),
                    Response = EResponse.Success
                };
            }
            catch (Exception ex)
            {

                return new MessagePayload<HttpGetUserResponse>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<IEnumerable<HttpGetUserNamesByDepartmentsResponse>>> GetUserNamesByDepartments(int departmentId)
        {
            try
            {
                return new MessagePayload<IEnumerable<HttpGetUserNamesByDepartmentsResponse>>
                {
                    Status = 200,
                    Payload = await _userContext.GetUserNamesByDepartments(departmentId),
                    Response = EResponse.Success
                };
            }
            catch (Exception ex)
            {

                return new MessagePayload<IEnumerable<HttpGetUserNamesByDepartmentsResponse>>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<IEnumerable<HttpGetUserResponse>>> GetUsers()
        {
            try
            {
                return new MessagePayload<IEnumerable<HttpGetUserResponse>>
                {
                    Status = 200,
                    Payload = await _userContext.GetUsers(),
                    Response = EResponse.Success
                };
            }
            catch (Exception ex)
            {

                return new MessagePayload<IEnumerable<HttpGetUserResponse>>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        public int ValidateToken(string token)
        {
            try
            {
               var claimsPrincipal = _tokenService.ValidateToken(token);
                if (claimsPrincipal == null)
                    return 401;
                return 200;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
