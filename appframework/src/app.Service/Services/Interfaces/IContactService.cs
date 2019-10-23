using app.Data.Entities;
using app.Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Service.Services.Interfaces
{
    public interface IContactService
    {
        public ServiceResult AddNewContactEntity(Contact entity);

        public ServiceResult UpdateContactEntity(Contact entity);

        public ServiceResult DeleteContactEntity(int id);

        public ServiceResult<List<Contact>> GetAllContacts();

        public ServiceResult<Contact> GetContact(int id);


    }
}
