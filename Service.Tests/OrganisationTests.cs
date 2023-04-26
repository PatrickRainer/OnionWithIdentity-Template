using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Contracts;
using Domain.Exceptions;
using NUnit.Framework;

namespace Service.Tests;

public class OrganisationTests : ServiceTestBase
{
    [Test]
    public async Task CreateOrganisation()
    {
        var organisationForCreationDto = new OrganisationForCreationDto
        {
            Name = "Test Organisation"
        };

        var organisationDto = await ServiceManager.OrganisationService.CreateAsync(organisationForCreationDto,
            CancellationToken.None);

        Assert.IsNotNull(organisationDto);
        Assert.AreEqual(organisationForCreationDto.Name, organisationDto.Name);
    }

    // Test Get All Organisations
    [Test]
    public async Task GetAllOrganisations()
    {
        var organisationForCreationDto = new OrganisationForCreationDto
        {
            Name = "Test Organisation"
        };

        await ServiceManager.OrganisationService.CreateAsync(organisationForCreationDto,
            CancellationToken.None);

        var organisations = await ServiceManager.OrganisationService.GetAllAsync(CancellationToken.None);

        Assert.IsNotNull(organisations);
        Assert.AreEqual(2, organisations.Count()); // Expecting 2 because of the default organisation
    }

    // Test Get Organisation By Id
    [Test]
    public async Task GetOrganisationById()
    {
        var organisationForCreationDto = new OrganisationForCreationDto
        {
            Name = new Faker().Company.CompanyName()
        };

        var organisationDto = await ServiceManager.OrganisationService.CreateAsync(organisationForCreationDto,
            CancellationToken.None);

        var organisation =
            await ServiceManager.OrganisationService.GetByIdAsync(organisationDto.Id, CancellationToken.None);

        Assert.IsNotNull(organisation);
        Assert.AreEqual(organisationForCreationDto.Name, organisation.Name);
    }

    // Test Update Organisation
    [Test]
    public async Task UpdateOrganisation()
    {
        var organisationForCreationDto = new OrganisationForCreationDto
        {
            Name = new Faker().Company.CompanyName()
        };

        var organisationDto = await ServiceManager.OrganisationService.CreateAsync(organisationForCreationDto,
            CancellationToken.None);

        var organisationForUpdateDto = new OrganisationForUpdateDto
        {
            Name = new Faker().Company.CompanyName()
        };

        await ServiceManager.OrganisationService.UpdateAsync(organisationDto.Id, organisationForUpdateDto,
            CancellationToken.None);

        var organisation =
            await ServiceManager.OrganisationService.GetByIdAsync(organisationDto.Id, CancellationToken.None);

        Assert.IsNotNull(organisation);
        Assert.AreEqual(organisationForUpdateDto.Name, organisation.Name);
    }

    // Test Delete Organisation
    [Test]
    public async Task DeleteOrganisation()
    {
        var organisationForCreationDto = new OrganisationForCreationDto
        {
            Name = new Faker().Company.CompanyName()
        };

        var organisationDto = await ServiceManager.OrganisationService.CreateAsync(organisationForCreationDto,
            CancellationToken.None);

        await ServiceManager.OrganisationService.DeleteAsync(organisationDto.Id, CancellationToken.None);

        Assert.ThrowsAsync<OrganisationNotFoundException>(async () =>
            await ServiceManager.OrganisationService.GetByIdAsync(organisationDto.Id, CancellationToken.None));
    }
}