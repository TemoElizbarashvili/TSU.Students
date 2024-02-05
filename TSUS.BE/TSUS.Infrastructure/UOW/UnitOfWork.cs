using Microsoft.Extensions.Configuration;
using TSUS.Domain.DataBase;
using TSUS.Infrastructure.Repositories;
using TSUS.Infrastructure.UOW.Contract;

namespace TSUS.Infrastructure.UOW;

public class UnitOfWork(TsusDbContext context, IConfiguration configuration) : IUnitOfWork
{
    private readonly TsusDbContext _context = context;
    public UserRepository UserRepository { get; set; } = new(context, configuration);
    public FacultyRepository FacultyRepository { get; set; } = new(context);
    public DepartmentRepository DepartmentRepository { get; set; } = new(context);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}