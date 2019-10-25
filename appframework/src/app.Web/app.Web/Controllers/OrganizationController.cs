using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Core.Extensions;
using app.Infra.Models.Organizations;
using app.Service.Services.Base;
using app.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Web.Controllers
{
    [Authorize]
    [Route("organization")]
    public class OrganizationController : Controller
    {
        private IOrganizationService _organizationService;

        private IServiceResult ServiceResultModel { get; set; }

        public OrganizationController(IOrganizationService organization)
        {
            _organizationService = organization;
        }

        #region Organizations - View List

        [HttpGet, Route("view-organizations")]
        public IActionResult ViewOrganizations()
        {
            return View();
        }

        [HttpGet, Route("view-organization-partial")]
        public IActionResult ViewOrganizationPartial()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            var model = new List<OrganizationDetailsViewModel>();
            model.AddRange(_organizationService.GetAllOrganizations().Data
                .Select(o => new OrganizationDetailsViewModel(o)));
            return PartialView("_ViewOrganizationPartial", model);
        }

        #endregion

        #region Organizations - Modal Selector Table

        [HttpGet, Route("view-organization-selector-partial")]
        public IActionResult ViewOrganizationSelectorPartial()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            var model = new List<OrganizationDetailsViewModel>();
            model.AddRange(_organizationService.GetAllOrganizations().Data
                .Select(o => new OrganizationDetailsViewModel(o)));
            return PartialView("_ViewOrganizationSelectorPartial", model);
        }

        #endregion

        #region Organizations - Add New Entry

        //Ajax - New Entry Form
        [HttpGet, Route("new-Organization-partial")]
        public IActionResult NewOrganizationPartial()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            var model = new OrganizationNewViewModel();
            return PartialView("_NewOrganizationPartial", model);
        }

        [HttpPost, Route("new-Organization-partial")]
        public async Task<IActionResult> NewOrganizationPartial(OrganizationNewViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            if (model == null) return PartialView("_FailureToLoadDataPartial");

            ViewBag.HttpMethod = Request.Method;

            if (ModelState.IsValid)
            {
                var entity = model.GetBase();

                ServiceResultModel = await _organizationService.AddNewOrganizationEntityAsync(entity);

                if (ServiceResultModel.IsSuccess)
                {
                    return PartialView("_SuccessAddPartial");
                }
                else
                {
                    model.ErrorMessage = ServiceResultModel.ErrorMessage;
                }
            }
            
            return PartialView("_NewOrganizationPartial", model);
        }

        //End - Ajax - New Entry Form


        #endregion
            
        #region Organizations - Update Entry

        //Ajax - Update Entry Form
        [HttpGet, Route("update-Organization-partial")]
        public IActionResult UpdateOrganizationPartial(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            if (id == 0) return PartialView("_FailureToLoadDataPartial");

            var OrganizationEntity = _organizationService.GetOrganization(id).Data;

            if (OrganizationEntity == null)
                return PartialView("_FailureToLoadDataPartial");

            var model = new OrganizationUpdateViewModel(OrganizationEntity);
            
            return PartialView("_UpdateOrganizationPartial", model);
        }

        [HttpPost, Route("update-Organization-partial")]
        public async Task<IActionResult> UpdateOrganizationPartial(OrganizationUpdateViewModel model, string command)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            if (model == null) return PartialView("_FailureToLoadDataPartial");

            ViewBag.HttpMethod = Request.Method;

            var OrganizationEntity = _organizationService.GetOrganization(model.Id).Data;

            if (OrganizationEntity == null)
            {
                model.IsSuccess = false;
                model.ErrorMessage = "Entry doesn't exist.";
                return View(model);
            }
                   
            if (command == "Delete")
            {
                ServiceResultModel = await _organizationService.DeleteOrganizationEntityAsync(model.Id);

                if (ServiceResultModel.IsSuccess)
                {
                    return PartialView("_SuccessDeletePartial");
                }
                else
                {
                    model.ErrorMessage = ServiceResultModel.ErrorMessage;
                }
            }

            if (command == "Update" && ModelState.IsValid)
            {
                OrganizationEntity = model.UpdateBase(OrganizationEntity);

                ServiceResultModel = await _organizationService.UpdateOrganizationEntityAsync(OrganizationEntity);

                if (ServiceResultModel.IsSuccess)
                {
                    return PartialView("_SuccessUpdatePartial");
                }
                else
                {
                    model.ErrorMessage = ServiceResultModel.ErrorMessage;
                }
            }
            
            return PartialView("_UpdateOrganizationPartial", model);
        }      

        //End - Ajax - Update Entry Form

        #endregion

    }
}