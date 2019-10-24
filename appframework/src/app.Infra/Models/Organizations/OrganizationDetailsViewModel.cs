using app.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Infra.Models.Organizations
{
    public class OrganizationDetailsViewModel : Organization
    {
        public OrganizationDetailsViewModel(Organization entity)
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
               
    }
}
