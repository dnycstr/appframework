using app.Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Infra.Models.Manage
{
    ///View model for displaying user details
    public class UserViewModel : ServiceResult
    {

        public string Id { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string RoleName { get; set; }

        public string CompanyName { get; set; }

        public string DepartmentName { get; set; }

        public bool IsActive { get; set; }

    }
}
