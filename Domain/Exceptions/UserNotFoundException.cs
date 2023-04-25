namespace Domain.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string userId)
        : base($"The user with the identifier {userId} was not found.")
    {
    }
}