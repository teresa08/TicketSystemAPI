using System;
using System.Collections.Generic;

namespace TicketSystemApi.DB;

public partial class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int PriorityId { get; set; }

    public int CategoryId { get; set; }

    public int StateId { get; set; }

    public int DepartmentId { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? UserId { get; set; }

    public virtual Category Category { get; set; }

    public virtual Department Department { get; set; }

    public virtual Priority Priority { get; set; }

    public virtual State State { get; set; }

    public virtual User User { get; set; }
}
