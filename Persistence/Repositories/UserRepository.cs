using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    readonly RepositoryDbContext _dbContext;

    public UserRepository(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllByOrganisationIdAsync(Guid organisationId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.users.Where(x => x.OrganisationId == organisationId).ToListAsync(cancellationToken);
    }

    public async Task<User> GetByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
    }

    public void Insert(User user)
    {
        _dbContext.users.Add(user);
    }

    public void Remove(User user)
    {
        _dbContext.users.Remove(user);
    }
}