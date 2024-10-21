using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO.Category;
using Domain.DTO.Department;
using TicketSystemApi.Repositories.Ticket;

namespace Domain.DTO.Ticket
{
    public class HttpGetPropertyCreateTicketResponse
    {
        public IEnumerable<HttpGetAllCategoryNameResponse> CategoryNames { get; set; }

        public IEnumerable<HttpGetAllDepartmentNameResponse> DepartmentNames { get; set; }

        public IEnumerable<HttpGetAllPriorityNameResponse> PriorityNames { get; set; }
    }
}
