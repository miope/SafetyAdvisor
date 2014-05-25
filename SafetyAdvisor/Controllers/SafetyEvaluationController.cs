using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafetyAdvisor.Controllers
{
    [Authorize(Roles = "Administrators,Editors,Members,Users")]
    public class SafetyEvaluationController : Controller
    {
        //
        // GET: /SafetyEvaluation/
        public ActionResult Index()
        {
            return View();
        }
	}
}