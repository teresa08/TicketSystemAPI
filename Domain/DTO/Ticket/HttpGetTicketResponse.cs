using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Ticket
{
    public class HttpGetTicketResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; }

        public string Category { get; set; }

        public string State { get; set; }

        public string Department { get; set; }
        public string User { get; set; }
        public DateTime? Date { get; set; }

        public int PriorityId { get; set; }

        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; } = 5;
        public int DepartmentId { get; set; }

    }
}
