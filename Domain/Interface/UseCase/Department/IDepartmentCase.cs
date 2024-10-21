using Domain.DTO.Department;
using Domain.DTO;
using Domain.Request.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.UseCase.Department
{
    public interface IDepartmentCase
    {
        public Task<MessagePayload<IEnumerable<HttpGetAllDepartmentNameResponse>>> GetAllDepartment();
        public Task<MessagePayload<string>> CreateDepartment(CreateDepartmentRequest department);

        public Task<MessagePayload<int>> DeleteDepartment(int id);
        public Task<MessagePayload<int>> UpdateDepartment(int id, CreateDepartmentRequest department);
        public Task<MessagePayload<HttpGetAllDepartmentNameResponse>> GetDepartment(int id);
    }
}
