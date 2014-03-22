using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SafetyAdvisor.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        new public virtual IDbSet<ApplicationRole> Roles { get; set; }
        public virtual IDbSet<EvaluationItem> EvaluationItems { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public void DeleteRecursive(EvaluationItem evaluationitem)
        {
            foreach (var item in evaluationitem.Children.ToList())
            {
                DeleteRecursive(item);
            }

            this.EvaluationItems.Remove(evaluationitem);
        }
    }
}