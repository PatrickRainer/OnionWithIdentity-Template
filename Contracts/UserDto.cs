using System;

namespace Contracts;

public class UserDto
{
    public string Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Guid OrganisationId { get; set; }

    public DateTime DateCreated { get; set; }

    public string UserType { get; set; }
}