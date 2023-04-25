using System;
using Domain.Repositories;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    readonly Lazy<IUserRepository> _lazyUserRepository;
    readonly Lazy<IOrganisationRepository> _lazyOrganisationRepository;
    readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

    public RepositoryManager(RepositoryDbContext dbContext)
    {
        _lazyOrganisationRepository = new Lazy<IOrganisationRepository>(() => new OrganisationRepository(dbContext));
        _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(dbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
    }

    public IOrganisationRepository OrganisationRepository => _lazyOrganisationRepository.Value;

    public IUserRepository UserRepository => _lazyUserRepository.Value;

    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}