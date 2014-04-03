using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafetyAdvisor.Controllers
{
    [Authorize]
    public class SafetyCommunicationController : Controller
    {
        //
        // GET: /Communication/
        public ActionResult Index()
        {
            return View();
        }
	}
}