namespace WebApi.Application.Repository;

public record PagingOptions<T>(int PageCount, int ItemsPerPage, int CurrentPage, int ItemsCount, T Items);

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<PagingOptions<IEnumerable<T>>> GetAllWithPagingOptionsAsync(int page = 1, int pageSize = 20);
}
