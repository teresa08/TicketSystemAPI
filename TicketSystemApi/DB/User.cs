using System;
using System.Collections.Generic;

namespace TicketSystemApi.DB;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public int RoleId { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; }

    public virtual Role Role { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
