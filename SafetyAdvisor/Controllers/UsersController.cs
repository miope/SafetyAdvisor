using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafetyAdvisor.Models;

namespace SafetyAdvisor.Controllers
{
    [Authorize(Roles="Administrators")]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            return View(applicationuser);
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FirstName,LastName,Email,Company")] ApplicationUser applicationuser)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(applicationuser).State = EntityState.Modified;              
                var _user = db.Users.Find(applicationuser.Id);
                _user.FirstName = applicationuser.FirstName;
                _user.LastName = applicationuser.LastName;
                _user.Email = applicationuser.Email;
                _user.Company = applicationuser.Company;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationuser);
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
