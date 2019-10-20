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

namespace app.Web.Controllers
{
    [Authorize(Roles = "SysAdmin")]
    [Route("manage")]
    public class ManageController : BaseController
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IServiceResult ServiceResultModel { get; set; }
        private ApplicationDbContext _context { get; set; }
        public ManageController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        #region Manage Accounts

        [Route("accounts")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, Route("view-account-partial")]
        public ActionResult ViewAccountPartial()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            var model = new List<UserViewModel>();
            model.AddRange(_userManager.Users.ToList().Select(o => new UserViewModel
            {
                Id = o.Id,
                FirstName = o.FirstName,
                MiddleName = o.MiddleName,
                LastName = o.LastName,
                Username = o.UserName,
                IsActive = o.IsActive,
                //RoleName = _roleManager.FindByIdAsync(o.Roles.First().RoleId).Name
            }));

            return PartialView("_ViewAccountPartial", model);
        }

      
        #endregion

    }
}