using Domain.DTO.Department;
using Domain.DTO;
using Domain.Interface.Services;
using Domain.Interface.UseCase.Department;
using Microsoft.AspNetCore.Mvc;
using Domain.Request.Department;

namespace TicketSystemApi.Controllers
{
    [ApiController]
    [Route("")]
    public class DepartmentController : Controller
    {
        private IDepartmentCase _departmentCase;
        private ITokenService _tokenService;
        public DepartmentController(IDepartmentCase departmentCase, ITokenService tokenService)
        {
            _departmentCase = departmentCase;
            _tokenService = tokenService;

        }

        [HttpGet("departments")]
        public async Task<ActionResult<MessagePayload<IEnumerable<HttpGetAllDepartmentNameResponse>>>> GetAllDepartment()
        {
            try
            {
                var response = await _departmentCase.GetAllDepartment();
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<IEnumerable<HttpGetAllDepartmentNameResponse>>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

        [HttpPost("new-department")]

        public async Task<ActionResult<MessagePayload<string>>> CreateDepartment([FromBody] CreateDepartmentRequest department)
        {
            try
            {
                var response = await _departmentCase.CreateDepartment(department);
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

        [HttpDelete("delete-department/{id}")]
        public async Task<ActionResult<MessagePayload<int>>> DeleteDepartment([FromRoute] int id)
        {
            try
            {
                var response = await _departmentCase.DeleteDepartment(id);
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

        [HttpPut("update-department/{id}")]
        public async Task<ActionResult<MessagePayload<int>>> UpdateDepartment([FromRoute] int id, [FromBody]CreateDepartmentRequest department)
        {
            try
            {
                var response = await _departmentCase.UpdateDepartment(id, department);
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

        [HttpGet("update-department/{id}")]
        public async Task<ActionResult<MessagePayload<HttpGetAllDepartmentNameResponse>>> GetDepartment([FromRoute] int id)
        {
            try
            {
                var response = await _departmentCase.GetDepartment(id);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<HttpGetAllDepartmentNameResponse>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

    }
}
