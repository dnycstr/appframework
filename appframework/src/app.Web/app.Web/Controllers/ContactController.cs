using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Core.Extensions;
using app.Infra.Models.Contacts;
using app.Service.Services.Base;
using app.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactService _contactService;

        private IServiceResult ServiceResultModel { get; set; }

        public ContactController(IContactService contact)
        {
            _contactService = contact;
        }

        #region Contacts - View List

        [HttpGet, Route("view-contacts")]
        public ActionResult ViewContacts()
        {
            return View();
        }

        [HttpGet, Route("view-contact-partial")]
        public ActionResult ViewContactPartial()
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
        public ActionResult NewContactPartial()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            var model = new ContactNewViewModel();
            return PartialView("_NewContactPartial", model);
        }

        [HttpPost, Route("new-contact-partial")]
        public ActionResult NewContactPartial(ContactNewViewModel model)
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

                ServiceResultModel = _contactService.AddNewContactEntity(entity);

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

        //End - Ajax - New Entry Form


        #endregion


        #region Contacts - Update Entry

        //Ajax - Update Entry Form
        [HttpGet, Route("update-contact-partial")]
        public ActionResult UpdateContactPartial(int id)
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
            
            return PartialView("_UpdateContactPartial", model);
        }

        [HttpPost, Route("update-contact-partial")]
        public ActionResult UpdateContactPartial(ContactUpdateViewModel model, string command)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadRequest", "Home");
            }

            if (model == null) return PartialView("_FailureToLoadDataPartial");

            ViewBag.HttpMethod = Request.Method;

            var contactEntity = _contactService.GetContact(model.Id).Data;

            if (contactEntity == null)
            {
                model.IsSuccess = false;
                model.ErrorMessage = "Entry doesn't exist.";
                return View(model);
            }
                   
            if (command == "Delete")
            {
                ServiceResultModel = _contactService.DeleteContactEntity(model.Id);

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

                ServiceResultModel = _contactService.UpdateContactEntity(contactEntity);

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

        //End - Ajax - Update Entry Form

        #endregion

    }
}