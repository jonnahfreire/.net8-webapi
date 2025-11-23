namespace API.Application.Repository;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
