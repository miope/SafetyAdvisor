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

namespace SafetyAdvisor.Controllers
{
    [Authorize(Roles="Administrators,Editors")]
    public class EvaluationItemsController : Controller
    {
        private ApplicationDbContext db;

        public EvaluationItemsController() : this(new ApplicationDbContext())
        {
        }

        public EvaluationItemsController(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        // GET: /EvaluationItem/
        public ActionResult Index()
        {
            return View(db.EvaluationItems.ToList());
        }

        // GET: /EvaluationItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationItem evaluationitem = db.EvaluationItems.Find(id);
            if (evaluationitem == null)
            {
                return HttpNotFound();
            }
            return View(evaluationitem);
        }

        // GET: /EvaluationItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EvaluationItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Caption,Content")] EvaluationItem evaluationitem)
        {
            if (ModelState.IsValid)
            {
                db.EvaluationItems.Add(evaluationitem);
                db.SaveChanges();
                return RedirectToAction("Index").Alert(AlertType.Success, "Evaluation item has been created.");
            }

            return View(evaluationitem);
        }

        // GET: /EvaluationItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationItem evaluationitem = db.EvaluationItems.Find(id);
            if (evaluationitem == null)
            {
                return HttpNotFound();
            }
            return View(evaluationitem);
        }

        // POST: /EvaluationItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Caption,Content")] EvaluationItem evaluationitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluationitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index").Alert(AlertType.Success, "Evaluation item has been updated.");
            }
            return View(evaluationitem);
        }

        // GET: /EvaluationItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationItem evaluationitem = db.EvaluationItems.Find(id);
            if (evaluationitem == null)
            {
                return HttpNotFound();
            }
            return View(evaluationitem);
        }

        // POST: /EvaluationItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvaluationItem evaluationitem = db.EvaluationItems.Find(id);
            db.EvaluationItems.Remove(evaluationitem);
            db.SaveChanges();
            return RedirectToAction("Index").Alert(AlertType.Success, "Evaluation item has been deleted.");
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
