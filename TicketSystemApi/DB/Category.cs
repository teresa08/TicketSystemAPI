using System;
using System.Collections.Generic;

namespace TicketSystemApi.DB;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryName { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
