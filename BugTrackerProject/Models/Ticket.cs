using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerProject.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public DateTime? Updated { get; set; }
        public virtual Project Project { get; set; }
        public int ProjectId { get; set; }
        public virtual TicketType TicketTypes { get; set; }
        public int TicketTypeId { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public int TicketPriorityId { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public int TicketStatusId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser AssignedUser { get; set; }
        public string AssignedUserId { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
    }
}