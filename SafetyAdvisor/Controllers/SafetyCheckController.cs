using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SafetyAdvisor.Helpers;
using SafetyAdvisor.Models;

namespace SafetyAdvisor.Controllers
{
    [Authorize]
    public class SafetyCheckController : Controller
    {
        private ApplicationDbContext db;

        public SafetyCheckController()
            : this(new ApplicationDbContext())
        {
        }

        public SafetyCheckController(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        //
        // GET: /SafetyCheck/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /safetycheck/create
        public ActionResult Create()
        {
            var _model = new SafetyCheckViewModel();
            _model.PreviousItems = null;
            _model.CurrentItems = GetModel(db.EvaluationItems.Where(ei => ei.Parent == null));
            return View(_model);
        }

        //
        // POST: /safetycheck/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SafetyCheckViewModel model)
        {
            var _previousIds = model.GetPreviousItemsIds();
            var _currentIds = model.GetCurrentItemsIds();
            var _selectedIds = model.GetCurrentlySelected().Select(cs => cs.Id);

            bool _next = Request.Form.AllKeys.Contains("next");

            if (_next)
            {
                if (!model.GetCurrentlySelected().Any())
                {
                    model.CurrentItems = GetModel(db.EvaluationItems.Where(ei => _currentIds.Contains(ei.Id)));
                    return View(model).Alert(AlertType.Danger, "Sie müssen zumindest einen Item auswählen!");
                }

                model.PreviousItems = model.CurrentItems;
                model.CurrentItems = GetModel(db.EvaluationItems.Where(ei => _selectedIds.Contains(ei.ParentId.Value))).ToList();
            }

            return View(model);

        }

        private IEnumerable<SelectSafetyConceptEditorViewModel> GetModel(IEnumerable<EvaluationItem> evalItems)
        {
            return evalItems.Select(ei => new SelectSafetyConceptEditorViewModel()
            {
                Id = ei.Id,
                EvaluationItem = ei,
                Selected = false
            });
        }
    }
}