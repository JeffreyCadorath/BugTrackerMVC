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
    public class ProjectsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        UserHelper userHelper;
        ProjectHelper projectHelper;

        public ProjectsController()
        {
            userHelper = new UserHelper();
            projectHelper = new ProjectHelper();
        }

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        [Authorize(Roles =("Admin, Project Manager"))]
        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [Authorize(Roles =("Admin, Project Manager"))]
        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles =("Admin"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles =("Admin"))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = ("Admin"))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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

        //See UserHelper and ProjectHelper for reference to Functions called to add and remove users from roles
        [Authorize(Roles = ("Admin"))]
        public ActionResult AddToRole()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.role = new SelectList(db.Roles, "Name", "Name");
                ViewBag.user = new SelectList(db.Users, "Id", "Email");
                return View();
            }
            else
            {
                ViewBag.Message = "You do not have access to this Page ttyl";
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddToRole(string user, string role)
        {
            userHelper.AddUserToRole(role, user);
            return RedirectToAction("Index", "Projects");
        }

        [Authorize(Roles =("Admin"))]
        public ActionResult RemoveFromRole()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.role = new SelectList(db.Roles, "Name", "Name");
                ViewBag.user = new SelectList(db.Users, "Id", "Email");
                return View();
            }
            else
            {
                ViewBag.Message = "You do not have access to this Page ttyl";
                return View();
            }
        }

        [HttpPost]
        public ActionResult RemoveFromRole(string user, string role)
        {
            userHelper.RemoveUserRole(role, user);
            return RedirectToAction("Index", "Projects");
        }

        [Authorize(Roles =("Admin, Project Manager"))]
        public ActionResult AddDevToProject()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.project = new SelectList(db.Projects, "Id", "Name");
                ViewBag.user = new SelectList(db.Users, "Id", "Email");
                return View();
            }
            else
            {
                ViewBag.Message = "You do not have access to this Page ttyl";
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddDevToProject(int project, string user)
        {
            userHelper.AssignDevProject(project, user);
            return RedirectToAction("Index", "Projects");
        }

        [Authorize(Roles =("Admin, Project Manager"))]
        public ActionResult RemoveDevFromProject()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.project = new SelectList(db.Projects, "Id", "Name");
                ViewBag.user = new SelectList(db.Users, "Id", "Email");
                return View();
            }
            else
            {
                ViewBag.Message = "You do not have access to this Page ttyl";
                return View();
            }
        }

        [HttpPost]
        public ActionResult RemoveDevFromProject(int project, string user)
        {
            userHelper.RemoveDevProject(project, user);
            return RedirectToAction("Index", "Projects");
        }

        public ActionResult ShowMyProjects()
        {
            if (User.IsInRole("Developer"))
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                var projects = user.Projects.ToList();

                return View(projects);
            }
            else
            {
                ViewBag.Message = "You don't have permission to view this page";
                return View();
            }
        }
    }
}
