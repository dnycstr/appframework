using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace app.Data.Entities.AspNet
{
    public class ApplicationRole : IdentityRole
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
        /// </summary>
        public ApplicationRole() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ApplicationRole(string name) : base(name) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="description">The description.</param>
        public ApplicationRole(string roleName, int sequenceId, string description)
            : base(roleName)
        {
            SequenceId = sequenceId;
            Description = description;
        }

        public int SequenceId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [StringLength(1000)]
        public string Description { get; set; }

    }
}
