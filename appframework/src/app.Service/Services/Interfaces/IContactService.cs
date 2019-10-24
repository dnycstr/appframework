using app.Data.Entities;
using app.Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace app.Service.Services.Interfaces
{
    public interface IContactService
    {
        public Task<ServiceResult<int>> AddNewContactEntityAsync(Contact entity);

        public Task<ServiceResult> UpdateContactEntityAsync(Contact entity);

        public Task<ServiceResult> DeleteContactEntityAsync(int id);

        public ServiceResult<List<Contact>> GetAllContacts();

        public ServiceResult<Contact> GetContact(int id);
    }
}
