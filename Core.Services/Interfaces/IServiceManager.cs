using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Abstractions.MailService;

namespace Core.Services.Interfaces;

public interface IServiceManager
{
    IOrganisationService OrganisationService { get; }
    IUserService UserService { get; }
    IEmailService EmailService { get; }
    IUserManager<User> UserManager { get; }
    SignInManager<User> SignInManager { get; }
    IConfiguration Configuration { get; }
}

//https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
// Probably move the whole Identity and security to the infrastructure Layer