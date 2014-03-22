using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SafetyAdvisor.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        public string Company { get; set; }

        [NotMapped]
        [Display(Name="Full name")]
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

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        new public virtual IDbSet<ApplicationRole> Roles { get; set; }
        public virtual IDbSet<EvaluationItem> EvaluationItems { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}