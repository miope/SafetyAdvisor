using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafetyAdvisor.Models;
using SafetyAdvisor.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SafetyAdvisor.Controllers
{
    [Authorize(Roles="Administrators")]
    public class UsersController : Controller
    {
        private ApplicationDbContext db;

        public UsersController() : this(new ApplicationDbContext())
        {
        }

        public UsersController(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: /Users/
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationuser = db.Users.Find(id);
            if (applicationuser == null)
            {
                return HttpNotFound();
            }
            
            return View(new EditUserWithRolesViewModel(applicationuser, db.Roles));
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserWithRolesViewModel user)
        {
            if (ModelState.IsValid)
            {
                var _dbUser = db.Users.Find(user.Id);
                _dbUser.FirstName = user.FirstName;
                _dbUser.LastName = user.LastName;
                _dbUser.Email = user.Email;
                _dbUser.Company = user.Company;
                _dbUser.Roles.Clear();
                user.Roles.Where(ur => ur.Selected)
                          .ToList()
                          .ForEach(ur => _dbUser.Roles.Add(new IdentityUserRole() { RoleId = ur.Id, UserId = _dbUser.Id }));
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationuser = db.Users.Find(id);
            if (applicationuser == null)
            {
                return HttpNotFound();
            }
            return View(applicationuser);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationuser = db.Users.Find(id);
            if (applicationuser.UserName.ToLower() == "administrator")
            {
                ModelState.AddModelError(string.Empty, "You cannot delete 'administrator' user.");
                return View(applicationuser);
            }

            db.Users.Remove(applicationuser);
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
    }
}
