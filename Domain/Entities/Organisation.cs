using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Organisation
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public ICollection<User> Users { get; set; }
}