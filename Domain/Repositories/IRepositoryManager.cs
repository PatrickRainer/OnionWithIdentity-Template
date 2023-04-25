namespace Domain.Repositories;

public interface IRepositoryManager
{
    IOrganisationRepository OrganisationRepository { get; }

    IUserRepository UserRepository { get; }

    IUnitOfWork UnitOfWork { get; }
}