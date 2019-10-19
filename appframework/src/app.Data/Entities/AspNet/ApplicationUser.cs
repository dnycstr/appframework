using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace app.Data.Entities.AspNet
{
    public class ApplicationUser : IdentityUser
    {

        #region Custom Properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        [StringLength(50)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Return the full name of the user
        /// </summary>
        public virtual string FullName => FirstName + " " +
                                          MiddleName + " " +
                                          LastName;

        /// <summary>
        /// Gets or sets the picture URL.
        /// </summary>
        /// <value>
        /// The picture URL.
        /// </value>
        [StringLength(1000)]
        public string PictureUrl { get; set; }

        //Add necessary properties here

        public bool IsActive { get; set; }

        #endregion
               
    }
}
