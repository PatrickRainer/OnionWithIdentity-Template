using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Exceptions;

public sealed class UserCreationFailedException : Exception
{
    public UserCreationFailedException(string message)
        : base(message)
    {
    }

    public UserCreationFailedException(IEnumerable<IdentityError> errors)
    {
        var exceptionMessage = string.Empty;
        foreach (var identityError in errors)
            exceptionMessage += $"{identityError.Code}: {identityError.Description}{Environment.NewLine}";

        throw new Exception("User creation failed" + Environment.NewLine + exceptionMessage);
    }
}