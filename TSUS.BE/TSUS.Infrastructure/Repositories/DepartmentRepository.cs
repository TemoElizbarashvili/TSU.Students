using Microsoft.EntityFrameworkCore;
using TSUS.Domain.DataBase;
using TSUS.Domain.Dtos;
using TSUS.Domain.Entities;
using TSUS.Infrastructure.ControlFlags;
using TSUS.Infrastructure.Repositories.Contracts;

namespace TSUS.Infrastructure.Repositories;

public class DepartmentRepository(TsusDbContext context) : IRepository<Department>
{
    private readonly TsusDbContext _context = context;

    public async Task<IEnumerable<Department>> GetAllAsync(BaseControlFlags flags = BaseControlFlags.Basic)
        => flags switch
        {
            BaseControlFlags.Basic => await _context.Departments.ToListAsync(),
            BaseControlFlags.All => await _context.Departments.Include(d => d.Faculty).ToListAsync(),
            _ => await _context.Departments.ToListAsync()
        };

    public async Task AddSingleAsync(Department model)
        => await _context.AddAsync(model);


    public Task AddMultipleAsync(IEnumerable<Department> models)
    {
        throw new NotImplementedException();
    }

    public void UpdateAsync(Department model)
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

    public Task<PagedListDto<Department>> PagedListAsync(int limit, int lastEntityId)
    {
        throw new NotImplementedException();
    }
}
