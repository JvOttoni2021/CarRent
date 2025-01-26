using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Infrastructure.Mappings
{
    public class CarMapping : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.Model)
                .HasColumnName("Model")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Maker)
                .HasColumnName("Maker")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Year)
                .HasColumnName("Year")
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Available)
                .HasColumnName("Available")
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.DailyPrice)
                .HasColumnName("DailyPrice")
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
