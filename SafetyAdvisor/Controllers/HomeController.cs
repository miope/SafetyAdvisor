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

        public ActionResult Repository()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                string _toEmail = ConfigurationManager.AppSettings["SafetyAdvisor.AdminEmailAddress"];
                MailMessage _emailMessage = new MailMessage(model.Email, _toEmail);
                _emailMessage.Subject = string.Format("SafetyAdvisor: Contact request from {0}", model.Email);
                _emailMessage.Body = model.GetContentsForEmail();

                SmtpClient _smtpClient = new SmtpClient();
                try
                {
                    _smtpClient.Send(_emailMessage);
                }
                catch (Exception ex)
                {
                    return View().Alert(AlertType.Danger, string.Format("Message could not be sent: {0}", ex.Message));
                }

                return View().Alert(AlertType.Success, "Thank you for your inquiry. We will be getting back to you as soon as we can.");
            }
            return View(model);
        }
    }
}