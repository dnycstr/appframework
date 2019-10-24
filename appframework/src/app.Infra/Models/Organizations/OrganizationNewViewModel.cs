using app.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Infra.Models.Organizations
{
    public class OrganizationNewViewModel : Organization
    {
        public OrganizationNewViewModel()
        {
 
        }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public Organization GetBase()
        {
            var entity = new Organization();
            
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
