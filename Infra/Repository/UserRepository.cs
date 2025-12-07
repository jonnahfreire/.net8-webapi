using Microsoft.EntityFrameworkCore;
using WebApi.Application.Repository;
using WebApi.Domain.Entities;
using WebApi.Infra.Database;

namespace WebApi.Infra.Repository;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    private readonly AppDbContext _context = context;

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }
}
