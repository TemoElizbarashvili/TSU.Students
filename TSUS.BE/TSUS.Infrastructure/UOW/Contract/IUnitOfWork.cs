using TSUS.Infrastructure.Repositories;
using TSUS.Infrastructure.Services;

namespace TSUS.Infrastructure.UOW.Contract;

public interface IUnitOfWork
{
    public UserRepository UserRepository { get; set; }
    public FacultyRepository FacultyRepository { get; set; }
    public DepartmentRepository DepartmentRepository { get; set; }
    public VerifyCodeService VerifyCodeRepository { get; set; }
    public Task SaveChangesAsync();
    public Task BeginTransactionAsync();
    public Task CommitAsync();
    public Task RollbackAsync();
    public Task DisposeAsync();
}