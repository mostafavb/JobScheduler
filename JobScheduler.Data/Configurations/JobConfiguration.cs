using JobScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobScheduler.Data.Configurations;
public class JobConfiguration : IEntityTypeConfiguration<Jobs>
{
    public void Configure(EntityTypeBuilder<Jobs> builder)
    {
        builder.ToTable("Jobs", "job");
        builder.Property(p => p.Name).HasMaxLength(50);
        builder.Property(p => p.QueueName).HasMaxLength(50);
        builder.Property(p => p.TypeFullName).HasMaxLength(150);
        builder.Property(p => p.TypeName).HasMaxLength(50);
        builder.Property(p => p.Cron)
            .HasMaxLength(20)
            .HasColumnType("varchar(20)");
    }
}
