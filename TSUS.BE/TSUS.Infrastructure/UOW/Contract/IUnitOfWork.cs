using TSUS.Infrastructure.Repositories;

namespace TSUS.Infrastructure.UOW.Contract;

public interface IUnitOfWork
{
    public UserRepository UserRepository { get; set; }
    public FacultyRepository FacultyRepository { get; set; }
    public DepartmentRepository DepartmentRepository { get; set; }
    public VerifyCodeRepository VerifyCodeRepository { get; set; }
    public Task SaveChangesAsync();
    public Task BeginTransactionAsync();
    public Task CommitAsync();
    public Task RollbackAsync();
    public Task DisposeAsync();
}