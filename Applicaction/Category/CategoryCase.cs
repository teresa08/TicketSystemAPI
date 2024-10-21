using Domain.DTO;
using Domain.DTO.Category;
using Domain.Interface.Repositories.Category;
using Domain.Interface.UseCase.Category;
using Domain.Request.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaction.Category
{
    public class CategoryCase : ICategoryCase
    {
        private ICategoryRepository _categoryRepositor;
        public CategoryCase(ICategoryRepository categoryRepositor)
        {
            _categoryRepositor = categoryRepositor;
        }
        public async Task<MessagePayload<string>> CreateCategory(CreateCategoryRequest category)
        {
            try
            {
                await _categoryRepositor.CreateCategory(category);
                return new MessagePayload<string>
                {
                    Status = 200,
                    Payload = "Ticket Creado",
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<string>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<int>> DeleteCategory(int id)
        {
            try
            {
                return new MessagePayload<int>
                {
                    Status = 200,
                    Payload = await _categoryRepositor.DeleteCategory(id),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<int>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<IEnumerable<HttpGetAllCategoryNameResponse>>> GetAllCategory()
        {
            try
            {
                return new MessagePayload<IEnumerable<HttpGetAllCategoryNameResponse>>
                {
                    Status = 200,
                    Payload = await _categoryRepositor.GetAllCategory(),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<IEnumerable<HttpGetAllCategoryNameResponse>>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<HttpGetAllCategoryNameResponse>> GetCategory(int id)
        {
            try
            {
                return new MessagePayload<HttpGetAllCategoryNameResponse>
                {
                    Status = 200,
                    Payload = await _categoryRepositor.GetCategory(id),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<HttpGetAllCategoryNameResponse>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<int>> UpdateCategory(int id, CreateCategoryRequest category)
        {
            try
            {
                return new MessagePayload<int>
                {
                    Status = 200,
                    Payload = await _categoryRepositor.UpdateCategory(id,category),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<int>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }
    }
}
