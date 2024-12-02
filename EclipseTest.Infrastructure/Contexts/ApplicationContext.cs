using EclipseTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EclipseTest.Infrastructure.Contexts;

public class ApplicationContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Project>? Projects { get; set; }
    public DbSet<Todo>? Todos { get; set; }
    public DbSet<TodoHistory>? Histories { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(x =>
        {
            x.HasKey(x => x.Id);
            x.HasIndex(x => x.Name)
                .IsUnique();
        });

        modelBuilder.Entity<Project>(x =>
        {
            x.HasKey(x => x.Id);

            x.HasMany(x => x.Tasks)
                .WithOne()
                .HasForeignKey(x => x.ProjectId);

            x.Navigation(x => x.Tasks)
                .AutoInclude();

            x.Navigation(x => x.CreatedBy)
                .AutoInclude();
        });

        modelBuilder.Entity<Todo>(x =>
        {
            x.HasKey(x => x.Id);

            x.HasMany(x => x.History)
                .WithOne()
                .HasForeignKey(x => x.TaskId);

            x.Navigation(x => x.History)
                .AutoInclude();

            x.Navigation(x => x.CreatedBy)
                .AutoInclude();
        });

        modelBuilder.Entity<TodoHistory>(x =>
        {
            x.HasKey(x => x.Id);
        });

        base.OnModelCreating(modelBuilder);
    }
}
