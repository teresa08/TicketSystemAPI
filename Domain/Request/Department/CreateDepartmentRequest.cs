using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request.Department
{
    public class CreateDepartmentRequest
    {
        public string DepartmentName { get; set; }

        public string Description { get; set; }
    }
}
