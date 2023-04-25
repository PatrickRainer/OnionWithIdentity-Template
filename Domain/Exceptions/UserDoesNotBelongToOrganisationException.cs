using System;

namespace Domain.Exceptions;

public sealed class UserDoesNotBelongToOrganisationException : BadRequestException
{
    public UserDoesNotBelongToOrganisationException(Guid organisationId, string userId)
        : base(
            $"The user with the identifier {userId} does not belong to the Organisation with the identifier {organisationId}")
    {
    }
}