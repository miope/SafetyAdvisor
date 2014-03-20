using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAdvisor.Models
{
    public class EditUserWithRolesViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Company { get; set; }
        public IEnumerable<SelectRoleEditorViewModel> Roles { get; private set; }

        public EditUserWithRolesViewModel()
        {
            this.Roles = new List<SelectRoleEditorViewModel>();
        }

        public EditUserWithRolesViewModel(ApplicationUser user, IEnumerable<ApplicationRole> roles)
            : this()
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Company = user.Company;

            this.Roles = roles.Select(r => new SelectRoleEditorViewModel(r) { Selected = user.Roles.Any(ur => ur.RoleId == r.Id) });
        }

    }

    public class SelectRoleEditorViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }

        public SelectRoleEditorViewModel()
        {
        }

        public SelectRoleEditorViewModel(ApplicationRole role)
        {
            this.Id = role.Id;
            this.Name = role.Name;
            this.Description = role.Description;
        }

    }

}