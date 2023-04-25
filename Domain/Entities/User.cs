using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    //User of User Id as type of Guid brings more Issues than it solves


    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime DateCreated { get; set; }

    public Guid OrganisationId { get; set; }
}