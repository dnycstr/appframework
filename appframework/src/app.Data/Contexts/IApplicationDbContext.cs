using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace app.Data.Contexts
{
    public interface IApplicationDbContext
    {

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);


    }
}
