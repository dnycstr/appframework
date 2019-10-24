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
    public class ContactService : BaseService, IContactService
    {
        public ContactService(IApplicationDbContext context) : base(context)
        {

        }

        #region Contact

        public async Task<ServiceResult<int>> AddNewContactEntityAsync(Contact entity)
        {
            if (entity == null)
                return Result(0, false, "Entry is invalid.");

            try
            {
                var contactEntity =
                    Context.Contacts.FirstOrDefault(o => !o.IsDeleted && o.Firstname == entity.Firstname && o.Lastname == entity.Lastname);

                if (contactEntity != null)
                    return Result(0, false, "Entry already exists.");

                contactEntity = new Contact
                {
                    Firstname = entity.Firstname,
                    Middlename = entity.Middlename,
                    Lastname = entity.Lastname,
                    
                    OrganizationId = entity.OrganizationId,
                    Position = entity.Position,
                    Email = entity.Email,
                    TelephoneNumber = entity.TelephoneNumber,
                    MobileNumber = entity.MobileNumber,
                };
                Context.Contacts.Add(contactEntity);
                await Context.SaveChangesAsync();

                return Result(contactEntity.Id, true);
            }
            catch (Exception e)
            {
                return Result(0, false, e.Message);
            }
        }

        public async Task<ServiceResult> UpdateContactEntityAsync(Contact entity)
        {
            if (entity == null)
                return Result(false, "Entry is invalid.");

            try
            {
                var contactEntity =
                Context.Contacts.FirstOrDefault(o => o.Id != entity.Id && !o.IsDeleted && o.Firstname == entity.Firstname && o.Lastname == entity.Lastname);

                if (contactEntity != null)
                    return Result(false, "Entry already exist.");

                contactEntity =
                 Context.Contacts.FirstOrDefault(o => o.Id == entity.Id && !o.IsDeleted);

                if (contactEntity == null)
                    return Result(false, "Entry doesn't exist.");

                // If everything is fine, update the entry
                contactEntity.Firstname = entity.Firstname;
                contactEntity.Middlename = entity.Middlename;
                contactEntity.Lastname = entity.Lastname;
                contactEntity.OrganizationId = entity.OrganizationId;
                contactEntity.Position = entity.Position;
                contactEntity.Email = entity.Email;
                contactEntity.TelephoneNumber = entity.TelephoneNumber;
                contactEntity.MobileNumber = entity.MobileNumber;

               await Context.SaveChangesAsync();

                return Result(true);
            }
            catch (Exception e)
            {
                return Result(false, e.Message);
            }
        }

        public async Task<ServiceResult> DeleteContactEntityAsync(int contactId)
        {
            if (contactId == 0)
                return Result(false, "Entry is invalid.");

            try
            {
                var contactEntity =
                 Context.Contacts.FirstOrDefault(o => o.Id == contactId && !o.IsDeleted);

                if (contactEntity == null)
                    return Result(false, "Entry doesn't exist.");

                // If everything is fine, update the entry
                contactEntity.IsDeleted = true;

                await Context.SaveChangesAsync();

                return Result(true);
            }
            catch (Exception e)
            {
                return Result(false, e.Message);
            }
        }

        public ServiceResult<List<Contact>> GetAllContacts()
        {
            return Result(Context.Contacts.Where(o => !o.IsDeleted).ToList(), true);
        }

        public ServiceResult<Contact> GetContact(int id)
        {
            return Result(Context.Contacts.FirstOrDefault(o => !o.IsDeleted && o.Id == id), true);
        }

        #endregion

    }
}
