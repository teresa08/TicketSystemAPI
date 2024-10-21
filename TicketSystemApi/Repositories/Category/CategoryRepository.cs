using Domain.DTO.Category;
using Domain.Interface.Repositories.Category;
using Domain.Interface.Repositories.Department;
using Domain.Request.Category;
using Microsoft.EntityFrameworkCore;
using TicketSystemApi.DB;

namespace TicketSystemApi.Repositories.Category
{
    public class CategoryRepository(TicketSystemDbContext ticketSystemDbContext) : ICategoryRepository
    {
        private readonly TicketSystemDbContext _ticketSystemDbContext = ticketSystemDbContext;

        public async Task<int> CreateCategory(CreateCategoryRequest category)
        {
            try
            {
                var search = await (from _category in _ticketSystemDbContext.Categories
                                    where _category.CategoryName == category.CategoryName
                                    select _category).FirstOrDefaultAsync();
                if (search == null)
                {
                    var newTicket = await _ticketSystemDbContext.Categories.AddAsync(new DB.Category
                    {
                        CategoryName = category.CategoryName

                    });
                    _ticketSystemDbContext.SaveChanges();
                    return newTicket.Entity.Id;
                }
                throw new Exception("El nombre de la categoría existe");

            }
            catch (Exception ex)
            {
                throw new Exception ($"{ex.Message}");
            }
        }

        public async Task<int> DeleteCategory(int id)
        {
            try
            {
                var categoryToDelete = await _ticketSystemDbContext.Categories
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (categoryToDelete != null)
                {
                    _ticketSystemDbContext.Categories.Remove(categoryToDelete);
                    await _ticketSystemDbContext.SaveChangesAsync();

                    return categoryToDelete.Id;
                }
                else
                {
                    throw new Exception("Category not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting category: {ex.Message}");
            }
        }

        public async Task<IEnumerable<HttpGetAllCategoryNameResponse>> GetAllCategory()
        {
            try
            {
                var categories = await _ticketSystemDbContext.Categories.ToListAsync();
                var categoriesResponses = categories.Select(c => new HttpGetAllCategoryNameResponse
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                }).ToList();
                return categoriesResponses;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<HttpGetAllCategoryNameResponse> GetCategory(int id)
        {
            try
            {
                var category = await (from _category in _ticketSystemDbContext.Categories
                                      where _category.Id == id
                                      select new HttpGetAllCategoryNameResponse
                                      {
                                          Id = _category.Id,
                                          CategoryName = _category.CategoryName,
                                      }).FirstOrDefaultAsync();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<int> UpdateCategory(int id, CreateCategoryRequest category)
        {
            try
            {
                var updateCategory = await (from _category in _ticketSystemDbContext.Categories
                                            where _category.Id == id
                                            select _category).FirstOrDefaultAsync();

                if (updateCategory != null)
                {
                    updateCategory.CategoryName = category.CategoryName;
                    await _ticketSystemDbContext.SaveChangesAsync();
                }
                return updateCategory.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
