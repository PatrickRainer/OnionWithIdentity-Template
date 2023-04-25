using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Persistence;
using Persistence.Repositories;
using Services;
using Services.Abstractions;

namespace Service.Tests;

public class ServiceTestBase
{
    public RepositoryDbContext DbContext;
    public IRepositoryManager RepositoryManager;
    public IServiceManager ServiceManager;

    public ServiceTestBase()
    {
        var inMemoryContextOptions = new DbContextOptionsBuilder<RepositoryDbContext>()
            .UseInMemoryDatabase("Onion_With_identity_inMemoryDb")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        DbContext = new RepositoryDbContext(inMemoryContextOptions);


        RepositoryManager = new RepositoryManager(DbContext);
        ServiceManager = new ServiceManager(RepositoryManager, MockHelpers.MockUserManager<User>().Object,
            MockHelpers.MockSignInManager<User>().Object, new Mock<IConfiguration>().Object);
    }

    [SetUp]
    public void Setup()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    [TearDown]
    public void TearDown()
    {
        DbContext.Database.EnsureDeleted();
    }
}