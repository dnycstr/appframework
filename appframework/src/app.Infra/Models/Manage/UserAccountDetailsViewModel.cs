using app.Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Infra.Models.Manage
{
    /// <summary>
    /// View model for displaying user account details
    /// </summary>
    public class UserAccountDetailsViewModel : ServiceResult
    {
        /// <summary>
        /// User account userid (GUID)
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Username for login
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User email address
        /// </summary>
        public string Email { get; set; }
               
        /// <summary>
        /// Display if user account is active
        /// </summary>
        public bool IsActive { get; set; }

    }
}
