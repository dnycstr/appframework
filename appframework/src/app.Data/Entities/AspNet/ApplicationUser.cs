using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace app.Data.Entities.AspNet
{
    public class ApplicationUser : IdentityUser
    {

        #region Custom Properties
              

        public bool IsActive { get; set; }

        #endregion
               
    }
}
