using Microsoft.EntityFrameworkCore;
using TSUS.Domain.DataBase;
using TSUS.Domain.Entities;
using TSUS.Infrastructure.Repositories.Contracts;

namespace TSUS.Infrastructure.Repositories;

public class DepartmentRepository(TsusDbContext context) : IRepository<Department>
{
    private readonly TsusDbContext _context = context;

    public async Task<IEnumerable<Department>> GetAllAsync()
        => await _context.Departments.Include(x => x.Faculty).ToListAsync();

    public async Task AddSingleAsync(Department model)
        => await _context.AddAsync(model);
    

    public Task AddMultipleAsync(IEnumerable<Department> models)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Department model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Department model)
    {
        throw new NotImplementedException();
    }

    public Task<Department?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}