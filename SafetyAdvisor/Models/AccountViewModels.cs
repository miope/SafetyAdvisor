using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SafetyAdvisor.Models
{
    public class ChagePasswordViewModel
    {
        [Required(ErrorMessage="Jetziges Kennwort ist ein Pflichtfeld.")]
        [DataType(DataType.Password)]
        [Display(Name = "Jetziges Kennwort")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage="Neues Kennwort ist ein Pflichtfeld.")]
        [StringLength(100, ErrorMessage = "Das {0} muss zumindest {2} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Neues Kennwort")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Kennwort wiederholen")]
        [Compare("NewPassword", ErrorMessage = "Die Kennwörter stimmen nich über ein.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage="Benutzername ist ein Pflichtfeld.")]
        [Display(Name = "Benutzername")]
        public string UserName { get; set; }

        [Required(ErrorMessage="Kennwort ist ein Pflichtfeld.")]
        [DataType(DataType.Password)]
        [Display(Name = "Kennwort")]
        public string Password { get; set; }

        [Display(Name = "Auf diesem Rechner eingelogt bleiben")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage="Benutzername ist ein Pflichtfeld.")]
        [Display(Name = "Benutzername")]
        public string UserName { get; set; }

        [Required(ErrorMessage="Kennwort ist ein Pflichtfeld.")]
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Kennwort")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Kennwort wiederholen")]
        [Compare("Password", ErrorMessage = "Die Kennwörter stimment nicht überein.")]
        public string ConfirmPassword { get; set; }
    }

}
