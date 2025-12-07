namespace WebApi.Application.Repository;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
