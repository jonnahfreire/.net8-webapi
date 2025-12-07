using Microsoft.EntityFrameworkCore;
using WebApi.Application.Repository;

namespace WebApi.Infra.Repository;

public class Repository<T>(DbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    async Task<PagingOptions<IEnumerable<T>>> IRepository<T>.GetAllWithPagingOptionsAsync(int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 20;
        if (pageSize > 50) throw new ArgumentException("PageSize cannot be greater than 50");

        int itemsCount = _dbSet.Count();
        int pageCount = (int)Math.Ceiling(itemsCount / (double)pageSize);
        int itemsPerPage = pageSize;
        int currentPage = page;

        var items = await _dbSet.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        var paging = new PagingOptions<IEnumerable<T>>(pageCount, itemsPerPage, currentPage, itemsCount, items);

        return paging;
    }
}
