using System;
using System.Collections.Generic;

namespace TicketSystemApi.DB;

public partial class Department
{
    public int Id { get; set; }

    public string DepartmentName { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
