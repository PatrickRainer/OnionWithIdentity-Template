using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllByOrganisationIdAsync(Guid organisationId,
        CancellationToken cancellationToken = default);

    Task<User> GetByIdAsync(string userId, CancellationToken cancellationToken = default);

    void Insert(User user);

    void Remove(User user);
}