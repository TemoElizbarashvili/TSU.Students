using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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
    public DbSet<VerifyCodes> VerifyCodes => Set<VerifyCodes>();
    public DbSet<ProfilePictures> ProfilePictures => Set<ProfilePictures>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(e => e.Role)
            .HasConversion(e => e.ToString(), e => (Role)Enum.Parse(typeof(Role), e)).HasMaxLength(15);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<VerifyCodes>().HasKey(c => new {c.Email, c.VerifyCode});
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ValidateEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ValidateEntities()
    {
        var entities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(e => e.Entity);

        foreach (var entity in entities)
        {
            var validationContext = new ValidationContext(entity);
            Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
        }
    }
}