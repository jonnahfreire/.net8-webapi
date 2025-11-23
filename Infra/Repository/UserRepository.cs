using API.Application.Repository;
using API.Domain.Entities;
using API.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Infra.Repository;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    private readonly AppDbContext _context = context;

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }
}
