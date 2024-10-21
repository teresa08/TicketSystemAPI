using System;
using System.Collections.Generic;

namespace TicketSystemApi.DB;

public partial class State
{
    public int Id { get; set; }

    public string StateName { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
