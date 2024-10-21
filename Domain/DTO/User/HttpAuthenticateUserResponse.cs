using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.User
{
    public class HttpAuthenticateUserResponse: HttpGetUserResponse
    {
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
    }
}
