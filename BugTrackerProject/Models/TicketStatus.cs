using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerProject.Models
{
    public class TicketStatus
    {
        public int Id { get; set; }
        public Completion Completion { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

    public enum Completion
    {
        Completed,
        InProgress
    }
}