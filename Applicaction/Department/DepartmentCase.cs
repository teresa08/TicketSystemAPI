using Domain.DTO;
using Domain.DTO.Department;
using Domain.Interface.Repositories.Department;
using Domain.Interface.Repositories.Ticket;
using Domain.Interface.UseCase.Department;
using Domain.Request.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaction.Department
{
    public class DepartmentCase: IDepartmentCase
    {
        private IDepartmentRepository _departmentRepository;
        public DepartmentCase(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<MessagePayload<string>> CreateDepartment(CreateDepartmentRequest department)
        {
            try
            {
                await _departmentRepository.CreateDepartment(department);
                return new MessagePayload<string>
                {
                    Status = 200,
                    Payload = "department Creado",
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

        public async Task<MessagePayload<int>> DeleteDepartment(int id)
        {
            try
            {
                return new MessagePayload<int>
                {
                    Status = 200,
                    Payload = await _departmentRepository.DeleteDepartment(id),
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

        public async Task<MessagePayload<IEnumerable<HttpGetAllDepartmentNameResponse>>> GetAllDepartment()
        {
            try
            {
                return new MessagePayload<IEnumerable<HttpGetAllDepartmentNameResponse>>
                {
                    Status = 200,
                    Payload = await _departmentRepository.GetAllDepartment(),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<IEnumerable<HttpGetAllDepartmentNameResponse>>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<HttpGetAllDepartmentNameResponse>> GetDepartment(int id)
        {
            try
            {
                return new MessagePayload<HttpGetAllDepartmentNameResponse>
                {
                    Status = 200,
                    Payload = await _departmentRepository.GetDepartment(id),
                    Response = EResponse.Success,
                };

            }
            catch (Exception ex)
            {
                return new MessagePayload<HttpGetAllDepartmentNameResponse>
                {
                    Status = 500,
                    ErrorCode = ex.Message,
                    Response = EResponse.Error
                };
            }
        }

        public async Task<MessagePayload<int>> UpdateDepartment(int id, CreateDepartmentRequest department)
        {
            try
            {
                return new MessagePayload<int>
                {
                    Status = 200,
                    Payload = await _departmentRepository.UpdateDepartment(id,department),
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
