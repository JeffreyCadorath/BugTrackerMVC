using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerProject.Models
{
    public class TicketHelper
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> usersManager;
        private RoleManager<IdentityRole> rolesManager;

        public TicketHelper()
        {
            usersManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        }

        public void UpdateTicket(string userId, int ticketId)
        {
            var user = db.Users.Find(userId);
            var ticket = db.Tickets.Find(ticketId);

            if(user != null && ticket != null)
            {
                ticket.TicketStatus.Completion = Completion.Completed;
                ticket.TicketPriority.Priority = Priority.Low;
            }
        }
    }
}