using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerProject.Models
{
    public class ProjectUsers
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public virtual Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}