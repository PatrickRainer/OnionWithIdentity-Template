using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services;

internal sealed class OrganisationService : IOrganisationService
{
    readonly IRepositoryManager _repositoryManager;

    public OrganisationService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<IEnumerable<OrganisationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var organisations = await _repositoryManager.OrganisationRepository.GetAllAsync(cancellationToken);

        var organisationsDto = organisations.Adapt<IEnumerable<OrganisationDto>>();

        return organisationsDto;
    }

    public async Task<OrganisationDto> GetByIdAsync(Guid organisationId, CancellationToken cancellationToken = default)
    {
        var organisation =
            await _repositoryManager.OrganisationRepository.GetByIdAsync(organisationId, cancellationToken);

        if (organisation is null) throw new OrganisationNotFoundException(organisationId);

        var organisationDto = organisation.Adapt<OrganisationDto>();

        return organisationDto;
    }

    public async Task<OrganisationDto> CreateAsync(OrganisationForCreationDto organisationForCreationDto,
        CancellationToken cancellationToken = default)
    {
        var organisation = organisationForCreationDto.Adapt<Organisation>();

        _repositoryManager.OrganisationRepository.Insert(organisation);

        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

        return organisation.Adapt<OrganisationDto>();
    }

    public async Task UpdateAsync(Guid organisationId, OrganisationForUpdateDto organisationForUpdateDto,
        CancellationToken cancellationToken = default)
    {
        var organisation =
            await _repositoryManager.OrganisationRepository.GetByIdAsync(organisationId, cancellationToken);

        if (organisation is null) throw new OrganisationNotFoundException(organisationId);

        organisation.Name = organisationForUpdateDto.Name;
        organisation.Address = organisationForUpdateDto.Address;

        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid organisationId, CancellationToken cancellationToken = default)
    {
        var organisation =
            await _repositoryManager.OrganisationRepository.GetByIdAsync(organisationId, cancellationToken);

        if (organisation is null) throw new OrganisationNotFoundException(organisationId);

        _repositoryManager.OrganisationRepository.Remove(organisation);

        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}