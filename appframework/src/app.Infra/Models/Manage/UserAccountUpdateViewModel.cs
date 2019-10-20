using app.Service.Services.Base;
using System.ComponentModel.DataAnnotations;

namespace app.Infra.Models.Manage
{
    /// <summary>
    /// View model for updating a user account entry
    /// </summary>
    public class UserAccountUpdateViewModel : ServiceResult
    {
        /// <summary>
        /// User account userid (GUID)
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User email and username for login
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
                      
        public bool IsActive { get; set; }
        
    }
}
