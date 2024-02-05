using TSUS.Infrastructure.Repositories;

namespace TSUS.Infrastructure.UOW.Contract;

public interface IUnitOfWork
{
    public UserRepository UserRepository { get; set; }
    public FacultyRepository FacultyRepository { get; set; }
    public DepartmentRepository DepartmentRepository { get; set; }
    public Task SaveChangesAsync();
}