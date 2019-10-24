using app.Data.Contexts;
using app.Data.Entities;
using app.Service.Services.Base;
using app.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Service.Services
{
    public class OrganizationService : BaseService, IOrganizationService
    {
        public OrganizationService(IApplicationDbContext context) : base(context)
        {

        }

        #region Organization

        public async Task<ServiceResult<int>> AddNewOrganizationEntityAsync(Organization entity)
        {
            if (entity == null)
                return Result(0, false, "Entry is invalid.");

            try
            {
                var organizationEntity =
                     Context.Organizations.FirstOrDefault(o => !o.IsDeleted && o.Name == entity.Name);

                if (organizationEntity != null)
                    return Result(0, false, "Entry already exists.");

                organizationEntity = new Organization
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    
                    TelephoneNumber = entity.TelephoneNumber,
                    MobileNumber = entity.MobileNumber,
                    Email = entity.Email,
                    Website = entity.Website,
                    Address1 = entity.Address1,
                    Address2 = entity.Address2,
                    AddressCity = entity.AddressCity,
                    AddressProvince = entity.AddressProvince,
                };
                Context.Organizations.Add(organizationEntity);
                await Context.SaveChangesAsync();

                return Result(organizationEntity.Id, true);
            }
            catch (Exception e)
            {
                return Result(0, false, e.Message);
            }
        }

        public async Task<ServiceResult> UpdateOrganizationEntityAsync(Organization entity)
        {
            if (entity == null)
                return Result(false, "Entry is invalid.");

            try
            {
                var organizationEntity =
                Context.Organizations.FirstOrDefault(o => o.Id != entity.Id && !o.IsDeleted && o.Name == entity.Name);

                if (organizationEntity != null)
                    return Result(false, "Entry already exist.");

                organizationEntity =
                 Context.Organizations.FirstOrDefault(o => o.Id == entity.Id && !o.IsDeleted);

                if (organizationEntity == null)
                    return Result(false, "Entry doesn't exist.");

                // If everything is fine, update the entry
                organizationEntity.Name = entity.Name;
                organizationEntity.Description = entity.Description;
                
                organizationEntity.TelephoneNumber = entity.TelephoneNumber;
                organizationEntity.MobileNumber = entity.MobileNumber;
                organizationEntity.Email = entity.Email;
                organizationEntity.Website = entity.Website;

                organizationEntity.Address1 = entity.Address1;
                organizationEntity.Address2 = entity.Address2;
                organizationEntity.AddressCity = entity.AddressCity;
                organizationEntity.AddressProvince = entity.AddressProvince;

                await Context.SaveChangesAsync();

                return Result(true);
            }
            catch (Exception e)
            {
                return Result(false, e.Message);
            }
        }

        public async Task<ServiceResult> DeleteOrganizationEntityAsync(int organizationId)
        {
            if (organizationId == 0)
                return Result(false, "Entry is invalid.");

            try
            {
                var organizationEntity =
                 Context.Organizations.FirstOrDefault(o => o.Id == organizationId && !o.IsDeleted);

                if (organizationEntity == null)
                    return Result(false, "Entry doesn't exist.");

                // If everything is fine, update the entry
                organizationEntity.IsDeleted = true;

                await Context.SaveChangesAsync();

                return Result(true);
            }
            catch (Exception e)
            {
                return Result(false, e.Message);
            }
        }

        public ServiceResult<List<Organization>> GetAllOrganizations()
        {
            return Result(Context.Organizations.Where(o => !o.IsDeleted).ToList(), true);
        }

        public ServiceResult<Organization> GetOrganization(int id)
        {
            return Result(Context.Organizations.FirstOrDefault(o => !o.IsDeleted && o.Id == id), true);
        }

        #endregion

    }
}
