using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class OrganisationRepository : IOrganisationRepository
{
    readonly RepositoryDbContext _dbContext;

    public OrganisationRepository(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Organisation>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Organisations.Include(x => x.Users).ToListAsync(cancellationToken);
    }

    public async Task<Organisation> GetByIdAsync(Guid organisationId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Organisations.Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Id == organisationId, cancellationToken);
    }

    public void Insert(Organisation organisation)
    {
        _dbContext.Organisations.Add(organisation);
    }

    public void Remove(Organisation organisation)
    {
        _dbContext.Organisations.Remove(organisation);
    }
}