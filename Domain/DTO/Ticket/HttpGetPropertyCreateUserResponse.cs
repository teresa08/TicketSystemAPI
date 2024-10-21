using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO.Department;

namespace Domain.DTO.Ticket
{
    public class HttpGetPropertyCreateUserResponse
    {
        public IEnumerable<HttpGetAllDepartmentNameResponse> DepartmentNames { get; set; }
        public IEnumerable<HttpGetAllRoleNameResponse> RoleNames { get; set; }
    }
}
