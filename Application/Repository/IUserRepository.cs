using WebApi.Domain.Entities;

namespace WebApi.Application.Repository;

public interface IUserRepository: IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
