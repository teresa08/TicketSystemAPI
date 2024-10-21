using Domain.DTO;
using Domain.DTO.User;
using Domain.Interface.Services;
using Domain.Interface.UseCase.User;
using Domain.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;
using TicketSystemApi.Services;
using TicketSystemApi.Settings;

namespace TicketSystemApi.Controllers
{
    [ApiController]
    [Route("")]
    public class UserController : Controller
    {
        private IUserCase _userCase;
        public UserController(IUserCase userCase)
        {
            _userCase = userCase;
        }

        [HttpPost("login")]

        public async Task<ActionResult<MessagePayload<HttpGetUserResponse>>> Login([FromBody] httpAuthenticateUserRequest user)
        {
            try
            {
                var response = await _userCase.AuthenticateUser(user);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
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

        [HttpPost("register")]
        [Authorize]
        public async Task<ActionResult<MessagePayload<string>>> Register([FromBody] CreateUserRequest user)
        {
            try
            {
                var response = await _userCase.CreateUser(user);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
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


        [HttpGet("users")]
        public async Task<ActionResult<MessagePayload<IEnumerable<HttpGetUserResponse>>>> GetUser()
        {
            try
            {
                var response = await _userCase.GetUsers();
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
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


        [HttpGet("user-names-by-department/{departmentId}")]
        public async Task<ActionResult<MessagePayload<IEnumerable<HttpGetUserNamesByDepartmentsResponse>>>> GetUserNamesByDepartments([FromRoute]int departmentId)
        {
            try
            {
                var response = await _userCase.GetUserNamesByDepartments(departmentId);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
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

        [HttpGet("validate-token")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token no encontrado" });
            }

            var principal = _userCase.ValidateToken(token);

            if (principal == 401)
            {
                return Unauthorized(new { message = "Token inválido o expirado" });
            }
            return Ok(new { message = "Token válido" });
        }

        [HttpGet("users/{id}/update")]
        public async Task<ActionResult<MessagePayload<HttpGetUserResponse>>> GetUser([FromRoute] int id)
        {
            try
            {
                var response = await _userCase.GetUser(id);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
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

        [HttpDelete("users/{id}/delete")]
        public async Task<ActionResult<MessagePayload<int>>> DeleteUser([FromRoute] int id)
        {
            try
            {
                var response = await _userCase.DeleteUser(id);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
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
    }
}
