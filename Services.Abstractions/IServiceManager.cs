using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Abstractions.MailService;

namespace Services.Abstractions;

public interface IServiceManager
{
    IOrganisationService OrganisationService { get; }
    IUserService UserService { get; }
    IEmailService EmailService { get; }
    UserManager<User> UserManager { get; }
    SignInManager<User> SignInManager { get; }
    IConfiguration Configuration { get; }
}