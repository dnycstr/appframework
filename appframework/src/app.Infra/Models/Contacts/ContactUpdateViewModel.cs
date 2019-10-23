using app.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Infra.Models.Contacts
{
    public class ContactUpdateViewModel : Contact
    {
        public ContactUpdateViewModel()
        {

        }

        public ContactUpdateViewModel(Contact entity)
        {
            this.Id = entity.Id;
            this.CreatedDate = entity.CreatedDate;
            this.ModifiedDate = entity.ModifiedDate;
            this.IsDeleted = entity.IsDeleted;

            this.Firstname = entity.Firstname;
            this.Middlename = entity.Middlename;
            this.Lastname = entity.Lastname;
            this.OrganizationId = entity.OrganizationId;
            this.Position = entity.Position;
            this.Email = entity.Email;
            this.TelephoneNumber = entity.TelephoneNumber;
            this.MobileNumber = entity.MobileNumber;
        }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public Contact UpdateBase(Contact entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;

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
