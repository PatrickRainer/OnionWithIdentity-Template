using System;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Services.Abstractions.MailService;
using Services.MailService;

namespace Services;

public sealed class ServiceManager : IServiceManager
{
    readonly Lazy<IUserService> _lazyUserService;
    readonly Lazy<IOrganisationService> _lazyOrganisationService;
    readonly Lazy<IEmailService> _lazyEmailService;


    public ServiceManager(IRepositoryManager repositoryManager, UserManager<User> userManager,
        SignInManager<User> signInManager, IConfiguration configuration)
    {
        //TODO: If possible instantiate these services with lazy
        UserManager = userManager;
        SignInManager = signInManager;
        Configuration = configuration;

        _lazyOrganisationService = new Lazy<IOrganisationService>(() => new OrganisationService(repositoryManager));
        _lazyUserService = new Lazy<IUserService>(() =>
            new UserService(repositoryManager, UserManager, SignInManager, Configuration));
        _lazyEmailService = new Lazy<IEmailService>(() => new EmailService());
        
    }

    public IOrganisationService OrganisationService => _lazyOrganisationService.Value;
    public IUserService UserService => _lazyUserService.Value;
    public IEmailService EmailService => _lazyEmailService.Value;
    public UserManager<User> UserManager { get; }
    public SignInManager<User> SignInManager { get; }
    public IConfiguration Configuration { get; }
}