using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SafetyAdvisor.Models;

namespace SafetyAdvisor.Controllers
{
    [Authorize(Roles = "Administrators,Editors,Members,Users")]
    public class SafetyConceptsController : Controller
    {
        private ApplicationDbContext db;

        public SafetyConceptsController()
            : this(new ApplicationDbContext())
        {
        }

        public SafetyConceptsController(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }
        //
        // GET: /SafetyConcepts/
        public ActionResult Index()
        {
            var _concepts = db.EvaluationItems.Where(ei => !ei.Children.Any())
                                              .Select(ei => new SafetyConceptModel() { EvaluationItem = ei });

            var _model = new SafetyConceptsViewModel()
            {
                PreventiveSafetyConcepts = _concepts.Where(c => !c.EvaluationItem.IsReactive),
                ReactiveSafetyConcepts = _concepts.Where(c => c.EvaluationItem.IsReactive)
            };

            return View(_model);
        }
    }
}