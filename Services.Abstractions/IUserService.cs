using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Domain.Entities;

namespace Services.Abstractions;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllByOrganisationIdAsync(Guid organisationId,
        CancellationToken cancellationToken = default);

    Task<UserDto> GetByIdAsync(Guid organisationId, string userId, CancellationToken cancellationToken);

    Task<UserDto> CreateAsync(Guid organisationId, UserForCreationDto userForCreationDto,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid organisationId, string userId, CancellationToken cancellationToken = default);
    Task<string> LoginAsync(UserLoginDto userLoginDto, CancellationToken cancellationToken = default);
    Task<User> GetUser(string userId);
}