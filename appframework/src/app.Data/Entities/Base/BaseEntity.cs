using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace app.Data.Entities.Base
{
    /// <summary>
    /// Base entity containing common entity properties.
    /// </summary>
    public class BaseEntity
    {

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the flag for virtual deletion of entry.
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets date when the entry was modified.
        /// </summary>
        public virtual DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets date when the entry was created.
        /// </summary>
        private DateTime _createdDate = DateTime.Now;

        public virtual DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
    }
}
