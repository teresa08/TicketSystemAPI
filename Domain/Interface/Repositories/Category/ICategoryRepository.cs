using Domain.DTO.Category;
using Domain.Request.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Repositories.Category
{
    public interface ICategoryRepository
    {
        public Task<int> CreateCategory(CreateCategoryRequest category);
        public Task<int> UpdateCategory(int id, CreateCategoryRequest category);
        public Task<int> DeleteCategory(int id);
        public Task<IEnumerable<HttpGetAllCategoryNameResponse>> GetAllCategory();
        public Task<HttpGetAllCategoryNameResponse> GetCategory(int id);
    }
}
