using Microsoft.EntityFrameworkCore;
using TSUS.Domain.DataBase;
using TSUS.Domain.Dtos;
using TSUS.Domain.Entities;
using TSUS.Infrastructure.Repositories.Contracts;

namespace TSUS.Infrastructure.Repositories;

public class FacultyRepository(TsusDbContext context) : IRepository<Faculty>
{
    private readonly TsusDbContext _context = context;

    public async Task<IEnumerable<Faculty>> GetAllAsync()
        => await _context.Faculties.ToListAsync();

    public async Task AddSingleAsync(Faculty model)
        => await _context.Faculties.AddAsync(model);

    public Task AddMultipleAsync(IEnumerable<Faculty> models)
    {
        throw new NotImplementedException();
    }

    public void UpdateAsync(Faculty model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Faculty model)
    {
        throw new NotImplementedException();
    }

    public Task<Faculty?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedListDto<Faculty>> PagedListAsync(int limit, int lastEntityId)
    {
        throw new NotImplementedException();
    }
}

