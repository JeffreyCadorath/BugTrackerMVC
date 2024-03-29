﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerProject.Models
{
    public class TicketType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}