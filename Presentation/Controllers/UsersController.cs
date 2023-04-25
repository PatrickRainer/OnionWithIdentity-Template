using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/Organisations/users")]
public class UsersController : ControllerBase
{
    readonly IServiceManager _serviceManager;

    public UsersController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(Guid organisationId,
        [FromBody] UserForCreationDto userForCreationDto, CancellationToken cancellationToken)
    {
        try
        {
            var response =
                await _serviceManager.UserService.CreateAsync(organisationId, userForCreationDto,
                    cancellationToken);

            return new OkObjectResult(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _serviceManager.UserService.LoginAsync(userLoginDto, cancellationToken);

            return new OkObjectResult(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


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

    #region Private Methods

    string GetUserIdFromToken()
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        if (claimsIdentity != null)
            return claimsIdentity.Claims.First(claim => claim.Type == "Id").Value;
        return "";
    }

    #endregion

    #region Endpoints for testing purposes

    [HttpGet("AuthorizationTest")]
    [Authorize]
    public ActionResult<string> AuthorizationTest()
    {
        if (Response.StatusCode == 401) return new UnauthorizedResult();

        return new OkObjectResult("Success");
    }

    [HttpGet("RoleTest")]
    [Authorize(Roles = "OrgUser")]
    public ActionResult<string> RoleTest()
    {
        if (Response.StatusCode == 401) return new UnauthorizedResult();

        return new OkObjectResult("Success");
    }

    #endregion
}