using Domain.DTO.Category;
using Domain.DTO;
using Domain.Request.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.UseCase.Category
{
    public interface ICategoryCase
    {
        public Task<MessagePayload<IEnumerable<HttpGetAllCategoryNameResponse>>> GetAllCategory();
        public Task<MessagePayload<string>> CreateCategory(CreateCategoryRequest category);
        public Task<MessagePayload<int>> UpdateCategory(int id, CreateCategoryRequest category);
        public Task<MessagePayload<int>> DeleteCategory(int id);
        public Task<MessagePayload<HttpGetAllCategoryNameResponse>> GetCategory(int id);

    }
}
