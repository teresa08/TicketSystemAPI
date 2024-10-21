using Applicaction.Category;
using Domain.DTO;
using Domain.DTO.Category;
using Domain.Interface.Services;
using Domain.Interface.UseCase.Category;
using Domain.Request.Category;
using Microsoft.AspNetCore.Mvc;

namespace TicketSystemApi.Controllers
{
    [ApiController]
    [Route("")]
    public class CategoryController : Controller
    {
        private ICategoryCase _categoryCase;
        private ITokenService _tokenService;
        public CategoryController(ICategoryCase categoryCase, ITokenService tokenService)
        {
            _categoryCase = categoryCase;
            _tokenService = tokenService;

        }

        [HttpPost("new-category")]

        public async Task<ActionResult<MessagePayload<string>>> CreateCategory([FromBody] CreateCategoryRequest category)
        {
            try
            {

                var response = await _categoryCase.CreateCategory(category);
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
        [HttpGet("categories")]
        public async Task<ActionResult<MessagePayload<IEnumerable<HttpGetAllCategoryNameResponse>>>> GetAllCategory()
        {
            try
            {
                var response = await _categoryCase.GetAllCategory();
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<IEnumerable<HttpGetAllCategoryNameResponse>>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }
        [HttpDelete("delete-category/{id}")]
        public async Task<ActionResult<MessagePayload<int>>> DeleteCategory([FromRoute]int id)
        {
            try
            {
                var response = await _categoryCase.DeleteCategory(id);
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
        [HttpPut("update-category/{id}")]
        public async Task<ActionResult<MessagePayload<int>>> UpdateCategory([FromRoute] int id, [FromBody] CreateCategoryRequest category)
        {
            try
            {
                var response = await _categoryCase.UpdateCategory(id, category);
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

        [HttpGet("category/{id}")]
        public async Task<ActionResult<MessagePayload<HttpGetAllCategoryNameResponse>>> GetCategory([FromRoute] int id)
        {
            try
            {
                var response = await _categoryCase.GetCategory(id);
                return response.Status == 200 ? Ok(response) : StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {

                return new MessagePayload<HttpGetAllCategoryNameResponse>
                {
                    ErrorCode = ex.ToString(),
                    Status = 500,
                    Response = EResponse.Error
                };
            }
        }

    }
}
