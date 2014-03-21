namespace SafetyAdvisor.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SafetyAdvisor.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SafetyAdvisor.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SafetyAdvisor.Models.ApplicationDbContext";
        }

        protected override void Seed(SafetyAdvisor.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Roles.AddOrUpdate(
                  r => r.Name,
                  new ApplicationRole("Administrators", "have full access to the system"),
                  new ApplicationRole("Editors", "can create, edit and delete content"),
                  new ApplicationRole("Members", "can access the project results repository"),
                  new ApplicationRole("Users", "can only access resources connected to their account (like evaluations)")
              );

            string _adminUserName = "administrator";

            UserManager<ApplicationUser> _um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            IdentityResult _res = _um.Create(new ApplicationUser() { UserName = _adminUserName }, "ChangeASAP!");
            if (_res.Succeeded)
            {
                _um.AddToRole(_um.FindByName(_adminUserName).Id, "Administrators");
            }
        }
    }
}
