using app.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Infra.Models.Contacts
{
    public class ContactNewViewModel : Contact
    {
        public ContactNewViewModel()
        {
            OrganizationSelectListItems = new List<SelectListItem>();
        }

        public List<SelectListItem> OrganizationSelectListItems { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public Contact GetBase()
        {
            var entity = new Contact();
            
            entity.Firstname = this.Firstname;
            entity.Middlename = this.Middlename;
            entity.Lastname = this.Lastname;
            entity.OrganizationId = this.OrganizationId;
            entity.Position = this.Position;
            entity.Email = this.Email;
            entity.TelephoneNumber = this.TelephoneNumber;
            entity.MobileNumber = this.MobileNumber;

            return entity;
        }

    }
}
