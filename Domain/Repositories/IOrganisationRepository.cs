using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories;

public interface IOrganisationRepository
{
    Task<IEnumerable<Organisation>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Organisation> GetByIdAsync(Guid organisationId, CancellationToken cancellationToken = default);

    void Insert(Organisation organisation);

    void Remove(Organisation organisation);
}