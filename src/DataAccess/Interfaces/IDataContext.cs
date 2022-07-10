using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccess.Interfaces;

public interface IDataContext
{
    DatabaseFacade Database { get; }

    DbContextId ContextId { get; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
}
