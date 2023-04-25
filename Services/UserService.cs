using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;

namespace Services;

internal sealed class UserService : IUserService
{
    readonly LoginHandler _loginHandler;
    readonly IRepositoryManager _repositoryManager;
    readonly UserManager<User> _userManager;

    public UserService(IRepositoryManager repositoryManager, UserManager<User> userManager,
        SignInManager<User> signInManager, IConfiguration configuration)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;

        _loginHandler = new LoginHandler(userManager, signInManager, configuration);
    }

    public async Task<UserDto> CreateAsync(Guid organisationId, UserForCreationDto userForCreationDto,
        CancellationToken cancellationToken = default)
    {
        return await _loginHandler.CreateAsync(organisationId, userForCreationDto, cancellationToken);
    }

    #region Login

    public async Task<string> LoginAsync(UserLoginDto userLoginDto, CancellationToken cancellationToken = default)
    {
        return await _loginHandler.LoginAsync(userLoginDto, cancellationToken);
    }

    #endregion

    public async Task<IEnumerable<UserDto>> GetAllByOrganisationIdAsync(Guid organisationId,
        CancellationToken cancellationToken = default)
    {
        var users =
            await _repositoryManager.UserRepository.GetAllByOrganisationIdAsync(organisationId, cancellationToken);

        var usersDto = users.Adapt<IEnumerable<UserDto>>();

        return usersDto;
    }

    public async Task<UserDto> GetByIdAsync(Guid organisationId, string userId,
        CancellationToken cancellationToken)
    {
        var organisation =
            await _repositoryManager.OrganisationRepository.GetByIdAsync(organisationId, cancellationToken);

        if (organisation is null) throw new OrganisationNotFoundException(organisationId);

        var user = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null) throw new UserNotFoundException(userId);

        if (user.OrganisationId != organisation.Id)
            throw new UserDoesNotBelongToOrganisationException(organisation.Id, user.Id);

        var userDto = user.Adapt<UserDto>();

        return userDto;
    }

    public async Task DeleteAsync(Guid organisationId, string userId, CancellationToken cancellationToken = default)
    {
        var deleteResult = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(userId));

        if (!deleteResult.Succeeded) throw new Exception("User Could not been deleted");
    }

    public async Task<User> GetUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new UserNotFoundException("User not found");

        return user;
    }
}