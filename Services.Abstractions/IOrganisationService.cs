using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;

namespace Services.Abstractions;

public interface IOrganisationService
{
    Task<IEnumerable<OrganisationDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<OrganisationDto> GetByIdAsync(Guid organisationId, CancellationToken cancellationToken = default);

    Task<OrganisationDto> CreateAsync(OrganisationForCreationDto organisationForCreationDto,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(Guid organisationId, OrganisationForUpdateDto organisationForUpdateDto,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid organisationId, CancellationToken cancellationToken = default);
}