using Domain.DTO.Department;
using Domain.Interface.Repositories.Department;
using Domain.Request.Department;
using Microsoft.EntityFrameworkCore;
using TicketSystemApi.DB;

namespace TicketSystemApi.Repositories.Department
{
    public class DepartmentRepository(TicketSystemDbContext ticketSystemDbContext) : IDepartmentRepository
    {
        private readonly TicketSystemDbContext _ticketSystemDbContext = ticketSystemDbContext;
        public async Task<int> DeleteDepartment(int id)
        {
            try
            {
                var departmentsToDelete = await _ticketSystemDbContext.Departments
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (departmentsToDelete != null)
                {
                    _ticketSystemDbContext.Departments.Remove(departmentsToDelete);
                    await _ticketSystemDbContext.SaveChangesAsync();

                    return departmentsToDelete.Id;
                }
                else
                {
                    throw new Exception("department not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting department: {ex.Message}");
            }
        }

        public async Task<int> CreateDepartment(CreateDepartmentRequest department)
        {
            try
            {
                var search = await (from _department in _ticketSystemDbContext.Departments
                                   where _department.DepartmentName == department.DepartmentName
                                   select _department).FirstOrDefaultAsync();

                if(search == null)
                {
                    var newTicket = await _ticketSystemDbContext.Departments.AddAsync(new DB.Department
                    {
                        DepartmentName = department.DepartmentName,
                        Description = department.Description,

                    });
                    _ticketSystemDbContext.SaveChanges();
                    return newTicket.Entity.Id;
                }
                throw new Exception("El nombre del departamento existe");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<HttpGetAllDepartmentNameResponse>> GetAllDepartment()
        {
            try
            {
                var department = await _ticketSystemDbContext.Departments.ToListAsync();
                var departmentResponses = department.Select(c => new HttpGetAllDepartmentNameResponse
                {
                    Id = c.Id,
                    DepartmentName = c.DepartmentName,
                    Description = c.Description,
                }).ToList();
                return departmentResponses;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<int> UpdateDepartment(int id, CreateDepartmentRequest department)
        {
            try
            {
                var updateDepartment = await (from _department in _ticketSystemDbContext.Departments
                                              where _department.Id == id
                                              select _department).FirstOrDefaultAsync();

                if (updateDepartment != null)
                {
                    updateDepartment.DepartmentName = department.DepartmentName;
                    updateDepartment.Description = department.Description;

                    await _ticketSystemDbContext.SaveChangesAsync();
                }
                return updateDepartment.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<HttpGetAllDepartmentNameResponse> GetDepartment(int id)
        {
            try
            {
                var department = await (from _department in _ticketSystemDbContext.Departments
                                        where _department.Id == id
                                        select new HttpGetAllDepartmentNameResponse
                                        {
                                            Id = _department.Id,
                                            DepartmentName = _department.DepartmentName,
                                        }).FirstOrDefaultAsync();
                return department;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
