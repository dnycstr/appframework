using app.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Infra.Models.Contacts
{
    public class ContactDetailsViewModel : Contact
    {
        public ContactDetailsViewModel(Contact entity)
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

        public string Fullname 
        {
            get 
            {
                return $"{Firstname} {Middlename} {Lastname}";
            }
        }
    }
}
