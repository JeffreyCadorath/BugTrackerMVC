using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerProject.Models
{
    public class Project
    {
        public Project()
        {
            this.ProjectUsers = new HashSet<ProjectUsers>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProjectUsers> ProjectUsers { get; set; }
    }
}