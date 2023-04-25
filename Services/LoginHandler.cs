using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Domain;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Services;

internal sealed class LoginHandler
{
    readonly IConfiguration _configuration;
    readonly SignInManager<User> _signInManager;
    readonly UserManager<User> _userManager;

    public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<UserDto> CreateAsync(Guid organisationId, UserForCreationDto userForCreationDto,
        CancellationToken cancellationToken = default)
    {
        var user = userForCreationDto.Adapt<User>();
        user.OrganisationId = organisationId;
        user.UserName = userForCreationDto.Email;

        var createResult = await _userManager.CreateAsync(user, userForCreationDto.Password);

        if (!createResult.Succeeded) throw new UserCreationFailedException(createResult.Errors);

        var addRoleResult = await _userManager.AddToRoleAsync(user, Values.OrgUserRole);

        if (!addRoleResult.Succeeded)
        {
            var deleteResult = await _userManager.DeleteAsync(user);
            throw new UserCreationFailedException(addRoleResult.Errors);
        }

        return user.Adapt<UserDto>();
    }

    public async Task<string> LoginAsync(UserLoginDto userLoginDto, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

        if (user is null) throw new UserNotFoundException(userLoginDto.Email);

        if (await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
        {
            var signInResult = await _signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password,
                userLoginDto.RememberMe, false);

            if (signInResult.Succeeded)
            {
                var signinCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                    _configuration.GetValue<string>("ApiValidIssuer"),
                    _configuration.GetValue<string>("ApiValidAudience"),
                    claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signinCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return token;
            }
        }
        else
        {
            throw new Exception("Password or Username not correct");
        }

        return "Login Error";
    }

    SigningCredentials GetSigningCredentials()
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetValue<string>("ApiSecretKey")));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new("Id", user.Id)
        };

        var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
        foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

        return claims;
    }
}