using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SafetyAdvisor.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Vorname")]
        public string FirstName { get; set; }

        [Display(Name = "Nachname")]
        public string LastName { get; set; }
        
        [EmailAddress(ErrorMessage="Email Adresse ist ungültig.")]
        public string Email { get; set; }
        public string Company { get; set; }

        [NotMapped]
        [Display(Name="Name")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }

        public ApplicationRole()
            : base()
        {
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {
        }

        public ApplicationRole(string roleName, string roleDescription)
            : base(roleName)
        {
            this.Description = roleDescription;
        }
    }
}