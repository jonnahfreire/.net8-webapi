using API.Domain.Entities;

namespace API.Application.Repository;

public interface IUserRepository: IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
