using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafetyAdvisor.Models;
using SafetyAdvisor.Helpers;
using System.Net.Mail;
using System.Configuration;

namespace SafetyAdvisor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Consortium()
        {
            return View();
        }

        public ActionResult Repository()
        {
            return View();
        }

        public ActionResult Contact()
        {
            var _model = new ContactForm();

            if (User.Identity.IsAuthenticated)
            {
                var _user = new ApplicationDbContext().Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (_user != null) {
                    _model.Name = _user.FullName;
                    _model.Email = _user.Email;
                }
            }

            return View(_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactForm model)
        {
            if (ModelState.IsValid)
            {

                string _subject = string.Format("SafetyAdvisor: Contact request from {0}", model.Email);
                string _body = model.GetContentsForEmail();
                try
                {
                    Mailer.SendEmailToAdmin(model.Email, _subject, _body);
                }
                catch (Exception ex)
                {
                    return View().Alert(AlertType.Danger, string.Format("Die Email konnte nich verschickt werden: {0}", ex.Message));
                }

                return View().Alert(AlertType.Success, "Vielen Dank für Ihre Anfrage! Wir melden uns bei Ihnen schnellstmöglich.");
            }
            return View(model);
        }
    }
}