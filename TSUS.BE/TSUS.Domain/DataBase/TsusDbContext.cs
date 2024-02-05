using Microsoft.EntityFrameworkCore;
using TSUS.Domain.Entities;

namespace TSUS.Domain.DataBase;

public class TsusDbContext : DbContext
{
    public TsusDbContext(DbContextOptions<TsusDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Faculty> Faculties => Set<Faculty>();
    public DbSet<Lecturer> Lecturers => Set<Lecturer>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Video> Videos => Set<Video>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(e => e.Role)
            .HasConversion(e => e.ToString(), e => (Role)Enum.Parse(typeof(Role), e)).HasMaxLength(15);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}
