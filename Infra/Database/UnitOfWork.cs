using API.Application.Repository;
namespace API.Infra.Database;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
