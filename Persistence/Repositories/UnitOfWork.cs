using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Persistence.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    readonly RepositoryDbContext _dbContext;

    public UnitOfWork(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}