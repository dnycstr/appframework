using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Core.Extensions;
using app.Infra.Models.Contacts;
using app.Service.Services.Base;
using app.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace app.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactService _contactService;

        private IOrganizationService _organizationService;

        private IServiceResult ServiceResultModel { get; set; }

        public ContactController(IContactService contact, IOrganizationService organization)
        {
            _contactService = contact;
            _organizationService = organization;
        }

        #region Contacts - View List

        [HttpGet, Route("view-contacts")]
        public IActionResult ViewContacts()
        {
            return View();
        }

        [HttpGet, Route("view-contact-partial")]
        public IActionResult ViewContactPartial()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            var model = new List<ContactDetailsViewModel>();
            model.AddRange(_contactService.GetAllContacts().Data
                .Select(o => new ContactDetailsViewModel(o)));
            return PartialView("_ViewContactPartial", model);
        }

        #endregion

        #region Contacts - Add New Entry

        //Ajax - New Entry Form
        [HttpGet, Route("new-contact-partial")]
        public IActionResult NewContactPartial()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            var model = new ContactNewViewModel();
            GetDefaultContactNewViewModel(model);
            return PartialView("_NewContactPartial", model);
        }

        [HttpPost, Route("new-contact-partial")]
        public async Task<IActionResult> NewContactPartial(ContactNewViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            if (model == null) return PartialView("_FailureToLoadDataPartial");

            ViewBag.HttpMethod = Request.Method;
            GetDefaultContactNewViewModel(model);

            if (ModelState.IsValid)
            {
                var entity = model.GetBase();

                ServiceResultModel = await _contactService.AddNewContactEntityAsync(entity);

                if (ServiceResultModel.IsSuccess)
                {
                    return PartialView("_SuccessAddPartial");
                }
                else
                {
                    model.ErrorMessage = ServiceResultModel.ErrorMessage;
                }
            }
            
            return PartialView("_NewContactPartial", model);
        }

        public void GetDefaultContactNewViewModel(ContactNewViewModel model)
        {
            var orglist = _organizationService.GetAllOrganizations().Data
            .Select(o => new { Value = o.Id, Text = o.Name }).ToList();

            model.OrganizationSelectListItems = new SelectList(orglist, "Value", "Text").ToList();
        }

        //End - Ajax - New Entry Form
        
        #endregion


        #region Contacts - Update Entry

        //Ajax - Update Entry Form
        [HttpGet, Route("update-contact-partial")]
        public IActionResult UpdateContactPartial(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            if (id == 0) return PartialView("_FailureToLoadDataPartial");

            var contactEntity = _contactService.GetContact(id).Data;

            if (contactEntity == null)
                return PartialView("_FailureToLoadDataPartial");

            var model = new ContactUpdateViewModel(contactEntity);
            GetDefaultContactUpdateViewModel(model);
            return PartialView("_UpdateContactPartial", model);
        }

        [HttpPost, Route("update-contact-partial")]
        public async Task<IActionResult> UpdateContactPartial(ContactUpdateViewModel model, string command)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            if (model == null) return PartialView("_FailureToLoadDataPartial");

            ViewBag.HttpMethod = Request.Method;
            GetDefaultContactUpdateViewModel(model);

            var contactEntity = _contactService.GetContact(model.Id).Data;

            if (contactEntity == null)
            {
                model.IsSuccess = false;
                model.ErrorMessage = "Entry doesn't exist.";
                return View(model);
            }
                   
            if (command == "Delete")
            {
                ServiceResultModel = await _contactService.DeleteContactEntityAsync(model.Id);

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
                contactEntity = model.UpdateBase(contactEntity);

                ServiceResultModel = await _contactService.UpdateContactEntityAsync(contactEntity);

                if (ServiceResultModel.IsSuccess)
                {
                    return PartialView("_SuccessUpdatePartial");
                }
                else
                {
                    model.ErrorMessage = ServiceResultModel.ErrorMessage;
                }
            }
            
            return PartialView("_UpdateContactPartial", model);
        }

        public void GetDefaultContactUpdateViewModel(ContactUpdateViewModel model)
        {
            var orglist = _organizationService.GetAllOrganizations().Data
            .Select(o => new { Value = o.Id, Text = o.Name }).ToList();

            model.OrganizationSelectListItems = new SelectList(orglist, "Value", "Text").ToList();
        }

        //End - Ajax - Update Entry Form

        #endregion

    }
}