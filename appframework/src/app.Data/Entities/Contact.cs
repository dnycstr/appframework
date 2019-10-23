using app.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace app.Data.Entities
{
    public class Contact : BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string Firstname { get; set; }

        [StringLength(250)]
        public string Middlename { get; set; }
        
        [StringLength(250)]
        public string Lastname { get; set; }
        
        public int? OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

        [StringLength(100)]
        public string Position { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(100)]
        public string TelephoneNumber { get; set; }

        [StringLength(100)]
        public string MobileNumber { get; set; }

    }
}
