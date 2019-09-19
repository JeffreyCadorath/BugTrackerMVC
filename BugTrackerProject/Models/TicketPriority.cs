using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerProject.Models
{
    public class TicketPriority
    {
        public int Id { get; set; }
        public Priority Priority { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

    public enum Priority
    {
        High,
        Moderate,
        Low
    }
}