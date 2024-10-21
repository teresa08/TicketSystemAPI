using Domain.DTO.Department;
using Domain.Request.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Repositories.Department
{
    public interface IDepartmentRepository
    {
        public Task<int> CreateDepartment(CreateDepartmentRequest department);
        public Task<IEnumerable<HttpGetAllDepartmentNameResponse>> GetAllDepartment();
        public Task<int> DeleteDepartment(int id);
        public Task<int> UpdateDepartment(int id, CreateDepartmentRequest department);
        public Task<HttpGetAllDepartmentNameResponse> GetDepartment(int id);
    }
}
