using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Services.Abstractions;

public interface IServiceManager
{
    IOrganisationService OrganisationService { get; }

    IUserService UserService { get; }
    UserManager<User> UserManager { get; }
    SignInManager<User> SignInManager { get; }
    IConfiguration Configuration { get; }
}