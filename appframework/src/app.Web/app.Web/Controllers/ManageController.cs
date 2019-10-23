using app.Core.Extensions;
using app.Data.Contexts;
using app.Data.Entities.AspNet;
using app.Infra.Models.Manage;
using app.Service.Services.Base;
using app.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Web.Controllers
{   
    [Authorize]
    [Route("manage")]
    public class ManageController : BaseController
    {        
        private readonly UserManager<ApplicationUser> _userManager;
        private IServiceResult ServiceResultModel { get; set; }
        public ManageController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        #region User Accounts - View List

        /// <summary>
        /// View the user accounts.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("view-user-accounts")]
        public ActionResult ViewUserAccounts()
        {
            return View();
        }

        /// <summary>
        /// Get the list of user accounts and 
        /// mapped the detail of the user accounts
        /// to the user account details view model.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("view-user-account-partial")]
        public ActionResult ViewUserAccountPartial()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            // Get all user accounts
            var users = _userManager.Users.ToList();

            // Map user details to the user account details view model
            var model = users.Select(o => new UserAccountDetailsViewModel
            {
                UserId = o.Id,
                Username = o.UserName,
                Email = o.Email,
                IsActive = o.IsActive,
            }).ToList();

            // Return the table of user accounts
            return PartialView("_ViewUserAccountPartial", model);
        }

        #endregion

        #region User Accounts - Add New Entry

        /// <summary> 
        /// Add new user account entry.
        /// Initialize add new entry view model.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("new-user-account-partial")]
        public ActionResult NewUserAccountPartial()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            // Initialize the new entry view model
            var model = new UserAccountNewViewModel();
            
            // Return the new entry form
            return PartialView("_NewUserAccountPartial", model);
        }

        /// <summary>
        /// Add new user account entry.
        /// Receive submitted new entry form.
        /// </summary>
        /// <returns> </returns>
        [ValidateAntiForgeryToken]
        [HttpPost, Route("new-user-account-partial")]
        public async Task<ActionResult> NewUserAccountPartial(UserAccountNewViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            ViewBag.HttpMethod = Request.Method;

            if (ModelState.IsValid)
            {
                // Initialize application user entity
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    IsActive = true
                };

                // Save entry
                var result = await _userManager.CreateAsync(user, model.Password);

                // Check if entry creation is successful
                if (result.Succeeded)
                {
                    //Return and notify success              
                    return PartialView("_SuccessAddPartial");
                }

                AddErrors(result);
            }

            // Return the submitted data with error message
            return PartialView("_NewUserAccountPartial", model);
        }


        #endregion

        #region User Accounts - Update Entry

        /// <summary>
        /// Update user account entry.
        /// Get the user account details and intialize the update view model.
        /// </summary>
        /// <param name="id">User account id</param>
        /// <returns>
        /// </returns>
        [HttpGet, Route("update-user-account-partial")]
        public async Task<ActionResult> UpdateUserAccountPartial(string id)
        {

            // Get the user account details
            var user = await _userManager.FindByIdAsync(id);

            // Validate if entry exist
            if (user == null)
                return PartialView("_FailureToLoadDataPartial");

            // Initialize the user account update view model
            var model = new UserAccountUpdateViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,              
                IsActive = user.IsActive,
            };
            
            // Return the update form view
            return PartialView("_UpdateUserAccountPartial", model);
        }

        /// <summary>
        /// Update user account entry.
        /// Receive the submitted update form.
        /// </summary>
        /// <param name="model">User account update view model</param>
        /// <param name="command">Update or Delete command</param>
        /// <returns></returns>
        [HttpPost, Route("update-user-account-partial")]
        public async Task<ActionResult> UpdateUserAccountPartial(UserAccountUpdateViewModel model, string command)
        {
            // Get selected user details
            var user = await _userManager.FindByIdAsync(model.UserId);

            // Validate if user exist
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return PartialView("_UpdateUserAccountPartial", model);
            }

            ViewBag.HttpMethod = Request.Method;

            // Check if command is update
            if (command == "Update")
            {
                if (ModelState.IsValid)
                {

                    // Update user details
                    user.Email = model.Email;                 
                    user.IsActive = model.IsActive;

                    
                    // Update user entry
                    var result = await _userManager.UpdateAsync(user);

                    // Check if entry update is successful
                    if (result.Succeeded)
                    {
                        //Return and notify success          
                        return PartialView("_SuccessUpdatePartial");
                    }

                    AddErrors(result);
                }

            }

            // If command is delete
            if (command == "Delete")
            {

                // Delete user entry
                var result = await _userManager.DeleteAsync(user);

                // Check if entry delete is successful
                if (result.Succeeded)
                {
                    //Return and notify success      
                    return PartialView("_SuccessDeletePartial");
                }

                AddErrors(result);
            }

            // Return the submitted data with error message
            return PartialView("_UpdateUserAccountPartial", model);
        }

        #endregion

        #region User Accounts - Reset Password

        /// <summary>
        /// Reset user account password.
        /// Get the user account details and intialize the reset password view model. 
        /// </summary>
        /// <param name="id">User account id</param>
        /// <param name="username">User account username</param>
        /// <returns></returns>
        [HttpGet, Route("reset-password-partial")]
        public ActionResult ResetPasswordPartial(string id, string username)
        {
            // Initialize user account reset password view model
            var model = new UserAccountResetPasswordViewModel
            {
                UserId = id,
                Username = username
            };

            // Return the reset password form view
            return PartialView("_ResetPasswordPartial", model);
        }

        /// <summary>
        /// Reset user account password.
        /// Receive the submitted reset password form.
        /// </summary>
        /// <param name="model">User account reset password view model</param>
        /// <returns></returns>
        [HttpPost, Route("reset-password-partial")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPasswordPartial(UserAccountResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ResetPasswordPartial", model);
            }

            // Get user details
            var user = await _userManager.FindByIdAsync(model.UserId);

            // Check if user exist
            if (user == null)
            {
                return PartialView("_FailureToLoadDataPartial");
            }

            // Generate password reset token
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Reset the user account password
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.Password);

            // Check if password is successful
            if (result.Succeeded)
            {
                // Return and notify success
                return PartialView("_SuccessUpdatePartial");
            }

            AddErrors(result);

            // Return the reset password form view with error
            return PartialView("_ResetPasswordPartial", model);
        }


        #endregion

    }
}