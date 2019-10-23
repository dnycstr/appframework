using app.Data.Entities.AspNet;
using app.Infra.Models.Account;
using app.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace app.Web.Controllers
{

    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = BuildLoginViewModelAsync(returnUrl);

            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputViewModel model, string button)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);
                if (result.Succeeded)
                {                  
                    // request for a local page
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        // user might have clicked on a malicious link - should be logged
                        throw new Exception("invalid return URL");
                    }
                }
            }

            // something went wrong, show form with error
            var vm = BuildLoginViewModelAsync(model);
            return View(vm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/
        private LoginViewModel BuildLoginViewModelAsync(string returnUrl)
        {
            return new LoginViewModel
            {
                AllowRememberLogin = true,
                EnableLocalLogin = true,
                ReturnUrl = returnUrl,
            };
        }

        private LoginViewModel BuildLoginViewModelAsync(LoginInputViewModel model)
        {
            var vm = BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }
    }
}