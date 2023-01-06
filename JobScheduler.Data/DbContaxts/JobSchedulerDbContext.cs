using JobScheduler.Data.Configurations;
using JobScheduler.Domain.Entities;
using JobScheduler.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JobScheduler.Data.DbContaxts;
public class JobSchedulerDbContext : DbContext
{
    public JobSchedulerDbContext()
    {

    }
    public JobSchedulerDbContext(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<Jobs> Jobs => Set<Jobs>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=SchedulerDB;Trusted_Connection=True;MultipleActiveResultSets=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //modelBuilder.ApplyConfiguration<Jobs>(new JobConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.CreatedBy = "MVB";
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.Now;
                    entry.Entity.LastModifiedBy = "MVB";
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}
