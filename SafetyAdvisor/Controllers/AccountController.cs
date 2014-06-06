using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SafetyAdvisor.Models;
using SafetyAdvisor.Helpers;

namespace SafetyAdvisor.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager) { AllowOnlyAlphanumericUserNames = false };
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    /*
                    result = await UserManager.AddToRoleAsync(user.Id, "Users");
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Manage").Alert(AlertType.Success, "You have registered for SafetyAdvisor. You can now edit your account data.");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                    */
                    await SignInAsync(user, isPersistent: false);
                    string _emailSubject = "SafetyAdvisor: New registration";
                    string _emailBody = string.Format("User {0} has registered for SafetyAdvisor. You need to approve his account.", user.UserName);
                    try
                    {
                        Mailer.SendEmailToAdmin("info@sichere-pharmakette.de", _emailSubject, _emailBody);
                    }
                    catch (Exception)
                    {
                        // do nothin for now...we will need to log this somewhere...later...
                    }
                    return RedirectToAction("Manage").Alert(AlertType.Success, "Sie haben sich erfolgreich registriert. Hier können Sie ihre Kontodaten bearbeiten. Erst nach der Freischaltung Ihres Kontos bekommen Sie vollen Zugang zu SafetyAdvisor Inhalten.");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePassword(string message)
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChagePasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                IdentityResult _result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (_result.Succeeded)
                {
                    return RedirectToAction("ChangePassword").Alert(AlertType.Success, "Your password has been changed.");
                }
                else
                {
                    AddErrors(_result);
                }
            }

            return View(model);
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage()
        {            
            var _user = UserManager.FindById(User.Identity.GetUserId());
            return View(_user);
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var _user = UserManager.FindById(User.Identity.GetUserId());
                _user.FirstName = model.FirstName;
                _user.LastName = model.LastName;
                _user.Email = model.Email;
                _user.Company = model.Company;

                IdentityResult result = await UserManager.UpdateAsync(_user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Manage").Alert(AlertType.Success, "Your account has been updated.");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}