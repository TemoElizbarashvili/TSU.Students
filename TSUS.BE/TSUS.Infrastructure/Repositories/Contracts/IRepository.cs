using TSUS.Domain.Dtos;

namespace TSUS.Infrastructure.Repositories.Contracts;

public interface IRepository<T> where T : class
{
    public Task AddSingleAsync(T model);
    public Task AddMultipleAsync(IEnumerable<T> models);
    public Task UpdateAsync(T model);
    public Task<bool> DeleteAsync(T model);
    public Task<T?> GetByIdAsync(int id);
    public Task<PagedListDto<T>> PagedListAsync(int limit, int lastEntityId);
}