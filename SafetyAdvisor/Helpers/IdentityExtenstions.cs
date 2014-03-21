using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SafetyAdvisor.Helpers
{
    public static class IdentityExtenstions
    {
        public static bool CanAccessRepository(this IPrincipal user)
        {
            return user.IsInAnyRole(new string[] {"Administrators", "Editors", "Members"});
        }

        public static bool CanAccessSiteSettings(this IPrincipal user)
        {
            return user.IsInRole("Administrators");
        }

        public static bool CanEditContent(this IPrincipal user)
        {
            return user.IsInAnyRole(new string[] { "Administrators", "Editors" });
        }

        public static bool IsInAnyRole(this IPrincipal user, IEnumerable<String> roles)
        {
            return roles.Any(r => user.IsInRole(r));
        }

        public static bool IsInAllRoles(this IPrincipal user, IEnumerable<String> roles)
        {
            return roles.All(r => user.IsInRole(r));
        }
    }

}