using app.Data.Entities;
using app.Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace app.Service.Services.Interfaces
{
    public interface IOrganizationService
    {
        public Task<ServiceResult<int>> AddNewOrganizationEntityAsync(Organization entity);

        public Task<ServiceResult> UpdateOrganizationEntityAsync(Organization entity);

        public Task<ServiceResult> DeleteOrganizationEntityAsync(int id);

        public ServiceResult<List<Organization>> GetAllOrganizations();

        public ServiceResult<Organization> GetOrganization(int id);
    }
}
