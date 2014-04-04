using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var _model = GetModel(null, true);
            return View(_model);
        }

        //
        // POST: /safetycheck/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<SelectSafetyConceptEditorViewModel> items)
        {
            bool _getChildren = Request.Form.AllKeys.Contains("children");
            IEnumerable<SelectSafetyConceptEditorViewModel> _model;
            if (_getChildren)
            {
                _model = GetModel(items.Where(m => m.Selected).Select(m => m.Id), true);
            }
            else
            {
                _model = GetModel(items.Select(m => m.Id), false);
            }
            return View(_model);
        }

        private IEnumerable<SelectSafetyConceptEditorViewModel> GetModel(IEnumerable<int> selected, bool children)
        {
            var _filter = children == true ? GetChildren(selected) : GetParents(selected);
            var _model = _filter.Select(ei => new SelectSafetyConceptEditorViewModel()
            {
                Id = ei.Id,
                EvaluationItem = ei
            });

            return _model;
        }

        private IEnumerable<EvaluationItem> GetChildren(IEnumerable<int> ids)
        {
            var _result = ids == null ? db.EvaluationItems.Where(ei => ei.Parent == null) : db.EvaluationItems.Where(ei => ids.Contains(ei.ParentId.Value));
            return _result;
        }

        private IEnumerable<EvaluationItem> GetParents(IEnumerable<int> ids)
        {
            var _result = db.EvaluationItems.Where(ei => ids.Contains(ei.Id)).Select(ei => ei.Parent).Distinct();
            return _result;
        }

    }
}