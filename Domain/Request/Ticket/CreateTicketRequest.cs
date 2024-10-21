using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request.Ticket
{
    public class CreateTicketRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int PriorityId { get; set; }

        public int CategoryId { get; set; }

        public int? UserId { get; set; }
        public int StateId { get; set; } = 5;
        public int DepartmentId { get; set; }
    }
}
