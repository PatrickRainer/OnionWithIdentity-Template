using System;
using System.Collections.Generic;

namespace Contracts;

public class OrganisationDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public IEnumerable<UserDto> Users { get; set; }
}