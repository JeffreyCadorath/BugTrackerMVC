using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerProject.Models
{
    public class UserHelper
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> usersManager;
        private RoleManager<IdentityRole> rolesManager;

        public UserHelper()
        {
            usersManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        }

        public bool AssignDevProject(int projectId, string userId)
        {
            var project = db.Projects.Find(projectId);
            var user = usersManager.FindById(userId);

            if (project == null || user == null)
            {
                return false;
            }
            else
            {
                ProjectUsers newProject = new ProjectUsers();
                newProject.User = user;
                newProject.Project = project;
                db.ProjectUsers.Add(newProject);
                db.SaveChanges();
                return true;
            }
        }

        public bool RemoveDevProject(int projectId, string userId)
        {
            var project = db.Projects.Find(projectId);
            var user = usersManager.FindById(userId);

            if (project == null || user == null)
            {
                return false;
            }
            else
            {
                ProjectUsers newProject = new ProjectUsers();
                newProject.User = user;
                newProject.Project = project;
                db.ProjectUsers.Remove(newProject);
                db.SaveChanges();
                return true;
            }
        }

        public bool AddUserToRole(string roleName, string userId)
        {
            var user = usersManager.FindById(userId);

            if (user != null
                && !usersManager.IsInRole(userId, roleName))
            {
                usersManager.AddToRole(userId, roleName);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveUserRole(string roleName, string userId)
        {
            var user = usersManager.FindById(userId);
            if (user == null)
            {
                return false;
            }
            else
            {
                usersManager.RemoveFromRole(userId, roleName);
                db.SaveChanges();
                return true;
            }
        }


    }
}