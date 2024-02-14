using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using TSUS.Domain.DataBase;
using TSUS.Infrastructure.Repositories;
using TSUS.Infrastructure.Services;
using TSUS.Infrastructure.UOW.Contract;

namespace TSUS.Infrastructure.UOW;

public class UnitOfWork(TsusDbContext context, IConfiguration configuration) : IUnitOfWork
{
    private readonly TsusDbContext _context = context;
    private IDbContextTransaction? _transaction;
    public UserRepository UserRepository { get; set; } = new(context, configuration);
    public FacultyRepository FacultyRepository { get; set; } = new(context);
    public DepartmentRepository DepartmentRepository { get; set; } = new(context);
    public VerifyCodeService VerifyCodeRepository { get; set; } = new(context);
    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _transaction!.CommitAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
    }

    public async Task RollbackAsync()
        => await _transaction!.RollbackAsync();

    public async Task DisposeAsync()
    {
        await _transaction!.DisposeAsync();
        await _context.DisposeAsync();
    }
}