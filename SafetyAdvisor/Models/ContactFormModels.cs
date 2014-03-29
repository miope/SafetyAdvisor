using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAdvisor.Models
{
    public class ContactForm
    {
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Message { get; set; }

        public string GetContentsForEmail()
        {
            string _emailContent = string.Empty;
            // some time later we will load this from a text file template
            // for now let's hardcode it here...
            _emailContent = string.Format("Name: {0}\r\nEmail: {1}\r\nPhone: {2}\r\n\r\nMessage:\r\n{3}", 
                    this.Name,
                    this.Email,
                    this.Phone,
                    this.Message
                );

            return _emailContent;
        }
    }
}