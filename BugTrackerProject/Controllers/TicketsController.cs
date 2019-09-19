using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTrackerProject.Models;
using Microsoft.AspNet.Identity;

namespace BugTrackerProject.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        UserHelper userHelper;
        TicketHelper ticketHelper;

        public TicketsController()
        {
            userHelper = new UserHelper();
            ticketHelper = new TicketHelper();
        }

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.AssignedUser).Include(t => t.Creator).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketTypes);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            if (User.IsInRole("Submitter"))
            {
                ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "Email");
                ViewBag.CreatorId = new SelectList(db.Users, "Id", "Email");
                ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
                ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Id");
                ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Id");
                ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Type");
                return View();
            }
            else
            {
                ViewBag.message = "You don't have permission to create new tickets";
                return View();
            }
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Creation,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,CreatorId,AssignedUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (User.IsInRole("Submitter"))
            {
                ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "Email", ticket.AssignedUserId);
                ViewBag.CreatorId = new SelectList(db.Users, "Id", "Email", ticket.CreatorId);
                ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
                ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Id", ticket.TicketPriorityId);
                ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Id", ticket.TicketStatusId);
                ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Type", ticket.TicketTypeId);
                return View(ticket);
            }
            else
            {
                ViewBag.message = "You don't have permission to make new tickets";
                return View();
            }
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles =("Submitter"))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "Email", ticket.AssignedUserId);
            ViewBag.CreatorId = new SelectList(db.Users, "Id", "Email", ticket.CreatorId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Id", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Id", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Type", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Creation,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,CreatorId,AssignedUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "Email", ticket.AssignedUserId);
            ViewBag.CreatorId = new SelectList(db.Users, "Id", "Email", ticket.CreatorId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Id", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Id", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Type", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles ="Developer, Submitter")]
        public ActionResult UpdateTicket(int ticketId)
        {
            if (User.IsInRole("Developer"))
            {
                var ticket = db.Tickets.Find(ticketId);
                return View(ticket);
            }
            else
            {
                ViewBag.Message = "You don't have permission to view this page GTFO";
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateTicket(int ticketId, string userId)
        {
            var user = db.Users.Find(userId);
            var ticket = db.Tickets.Find(ticketId);
            
            if(user != null && ticket != null)
            {
                ticketHelper.UpdateTicket(userId, ticketId);
                return RedirectToAction("Index", "Tickets");
            }
            else
            {
                ViewBag.Message = "You don't have permission to be here";
                return View();
            }
        }

        [Authorize(Roles =("Developer, Admin, Project Manager, Submitter"))]
        public ActionResult ShowYourTickets()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var tickets = db.Tickets.Where(x => x.AssignedUserId == user.Id).ToList();
            return View(tickets);
        }

        public ActionResult ShowYourTicketsSubmitter()
        {
            if (User.IsInRole("Submitter"))
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                var tickets = db.Tickets.Where(x => x.CreatorId == user.Id).ToList();
                return View(tickets);
            }
            else
            {
                ViewBag.message = "You don't have permission to view this page";
                return View();
            }
        }
    }
}
