using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SafetyAdvisor.Helpers
{
    public static class Mailer
    {
        public static void SendEmailToAdmin(string from, string subject, string body)
        {

            string _adminsEmail = GetAdminsEmailAddress();

            MailMessage _emailMessage = new MailMessage(from, _adminsEmail);
            _emailMessage.Subject = subject;
            _emailMessage.Body = body;

            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.Send(_emailMessage);
        }

        private static string GetAdminsEmailAddress()
        { 
                return ConfigurationManager.AppSettings["SafetyAdvisor.AdminEmailAddress"];
        }
    }
}