using System;
using System.Collections.Generic;

namespace TicketSystemApi.DB;

public partial class Priority
{
    public int Id { get; set; }

    public string PriorityName { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
