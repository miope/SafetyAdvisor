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
            _model.CurrentItems = GetModel(db.EvaluationItems.Where(ei => ei.Parent == null)
                                                             .GroupBy(ei => ei.Caption)
                                                             .Select(g => g.FirstOrDefault()));
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

            if (!model.GetCurrentlySelected().Any())
            {
                var _sameItems = db.EvaluationItems.Where(ei => _currentIds.Contains(ei.Id))
                                                   .GroupBy(ei => ei.Caption)
                                                   .Select(g => g.FirstOrDefault());

                model.CurrentItems = GetModel(_sameItems);
                return View(model).Alert(AlertType.Danger, "Sie müssen zumindest einen Item auswählen!");
            }

            model.PreviousItems = model.CurrentItems;
            var _childItems = db.EvaluationItems.Where(ei => _selectedIds.Contains(ei.ParentId.Value))
                                                .GroupBy(ei => ei.Caption)
                                                .Select(g => g.FirstOrDefault());

            model.CurrentItems = GetModel(_childItems);

            ModelState.Clear();
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