using System;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Controllers.Controllers;

[ApiController]
[Route("api/Organisations")]
public class OrganisationsController : ControllerBase
{
    readonly IServiceManager _serviceManager;

    public OrganisationsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> GetOrganisations(CancellationToken cancellationToken)
    {
        var organisations = await _serviceManager.OrganisationService.GetAllAsync(cancellationToken);

        return Ok(organisations);
    }

    [HttpGet("{organisationId:guid}")]
    [Authorize(Roles = "SuperAdmin, OrgAdmin")]
    public async Task<IActionResult> GetOrganisationById(Guid organisationId, CancellationToken cancellationToken)
    {
        var organisationDto = await _serviceManager.OrganisationService.GetByIdAsync(organisationId, cancellationToken);

        return Ok(organisationDto);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateOrganisation(
        [FromBody] OrganisationForCreationDto organisationForCreationDto)
    {
        var organisationDto = await _serviceManager.OrganisationService.CreateAsync(organisationForCreationDto);

        return CreatedAtAction(nameof(GetOrganisationById), new { OrganisationId = organisationDto.Id },
            organisationDto);
    }

    [HttpPut("{organisationId:guid}")]
    [Authorize(Roles = "SuperAdmin, OrgAdmin")]
    public async Task<IActionResult> UpdateOrganisation(Guid organisationId,
        [FromBody] OrganisationForUpdateDto organisationForUpdateDto, CancellationToken cancellationToken)
    {
        await _serviceManager.OrganisationService.UpdateAsync(organisationId, organisationForUpdateDto,
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{organisationId:guid}")]
    [Authorize(Roles = "SuperAdmin, OrgAdmin")]
    public async Task<IActionResult> DeleteOrganisation(Guid organisationId, CancellationToken cancellationToken)
    {
        await _serviceManager.OrganisationService.DeleteAsync(organisationId, cancellationToken);

        return NoContent();
    }
}