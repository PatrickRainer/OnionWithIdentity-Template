using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/Organisations/Users")]
public class OrgUserController : ControllerBase
{
    readonly IServiceManager _serviceManager;

    public OrgUserController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    #region Private Methods

    string GetUserIdFromToken()
    {
        if (HttpContext.User.Identity is ClaimsIdentity claimsIdentity)
            return claimsIdentity.Claims.First(claim => claim.Type == "Id").Value;
        return "";
    }

    #endregion

    [HttpGet("GetOrganisationUsers")]
    [Authorize(Roles = "SuperAdmin, OrgAdmin")]
    public async Task<IActionResult> GetOrganisationUsers(Guid organisationId, CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromToken();
        var user = await _serviceManager.UserService.GetUser(userId);

        if (user.OrganisationId != organisationId && !User.IsInRole("SuperAdmin"))
            return Unauthorized();

        var usersDto =
            await _serviceManager.UserService.GetAllByOrganisationIdAsync(organisationId, cancellationToken);

        return Ok(usersDto);
    }

    [HttpGet("{organisationId:guid}/{userId}")]
    [Authorize(Roles = "SuperAdmin, OrgAdmin")]
    public async Task<IActionResult> GetUserById(Guid organisationId, string userId,
        CancellationToken cancellationToken)
    {
        var user = await _serviceManager.UserService.GetUser(userId);

        if (user.OrganisationId != organisationId && !User.IsInRole("SuperAdmin"))
            return Unauthorized();

        var userDto =
            await _serviceManager.UserService.GetByIdAsync(organisationId, userId, cancellationToken);

        return Ok(userDto);
    }

    [HttpDelete("{organisationId:guid}/{userId}")]
    [Authorize(Roles = "SuperAdmin, OrgAdmin")]
    public async Task<IActionResult> DeleteUser(Guid organisationId, string userId,
        CancellationToken cancellationToken)
    {
        var user = await _serviceManager.UserService.GetUser(userId);

        if (user.OrganisationId != organisationId && !User.IsInRole("SuperAdmin"))
            return Unauthorized();

        await _serviceManager.UserService.DeleteAsync(organisationId, userId, cancellationToken);

        return NoContent();
    }
}