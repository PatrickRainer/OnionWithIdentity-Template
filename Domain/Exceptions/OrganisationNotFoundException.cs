using System;

namespace Domain.Exceptions;

public sealed class OrganisationNotFoundException : NotFoundException
{
    public OrganisationNotFoundException(Guid organisationId)
        : base($"The Organisation with the identifier {organisationId} was not found.")
    {
    }
}