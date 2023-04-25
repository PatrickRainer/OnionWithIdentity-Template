using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    //TODO: Can Id also be of type Guid?
    
    
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime DateCreated { get; set; }
    
    public Guid OrganisationId { get; set; }
}