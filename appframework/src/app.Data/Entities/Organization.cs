using app.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace app.Data.Entities
{
    public class Organization : BaseEntity
    {
        // Details

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
                     
        // Contact

        [StringLength(100)]
        public string TelephoneNumber { get; set; }

        [StringLength(100)]
        public string MobileNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Website { get; set; }


        // Address

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string AddressCity { get; set; }

        [StringLength(100)]
        public string AddressProvince { get; set; }
    }
}
