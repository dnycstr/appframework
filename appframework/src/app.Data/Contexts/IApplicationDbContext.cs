using app.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace app.Data.Contexts
{
    public interface IApplicationDbContext
    {

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
