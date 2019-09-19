using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerProject.Models
{
    public class ProjectHelper
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> usersManager;
        private RoleManager<IdentityRole> rolesManager;

        public ProjectHelper()
        {
            usersManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        }
    }
}