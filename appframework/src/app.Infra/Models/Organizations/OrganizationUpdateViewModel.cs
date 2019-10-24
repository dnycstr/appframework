using app.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Infra.Models.Organizations
{
    public class OrganizationUpdateViewModel : Organization
    {
        public OrganizationUpdateViewModel()
        {

        }

        public OrganizationUpdateViewModel(Organization entity)
        {
            this.Id = entity.Id;
            this.CreatedDate = entity.CreatedDate;
            this.ModifiedDate = entity.ModifiedDate;
            this.IsDeleted = entity.IsDeleted;

            this.Name = entity.Name;
            this.Description = entity.Description;

            this.TelephoneNumber = entity.TelephoneNumber;
            this.MobileNumber = entity.MobileNumber;
            this.Email = entity.Email;
            this.Website = entity.Website;

            this.Address1 = entity.Address1;
            this.Address2 = entity.Address2;
            this.AddressCity = entity.AddressCity;
            this.AddressProvince = entity.AddressProvince;
        }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public Organization UpdateBase(Organization entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;

            entity.Name = this.Name;
            entity.Description = this.Description;

            entity.TelephoneNumber = this.TelephoneNumber;
            entity.MobileNumber = this.MobileNumber;
            entity.Email = this.Email;
            entity.Website = this.Website;

            entity.Address1 = this.Address1;
            entity.Address2 = this.Address2;
            entity.AddressCity = this.AddressCity;
            entity.AddressProvince = this.AddressProvince;

            return entity;
        }
    }
}
