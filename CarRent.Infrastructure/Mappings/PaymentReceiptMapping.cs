using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Infrastructure.Mappings
{
    public class PaymentReceiptMapping : IEntityTypeConfiguration<PaymentReceipt>
    {
        public void Configure(EntityTypeBuilder<PaymentReceipt> builder)
        {
            builder.ToTable("PaymentReceipts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.RentalId)
                .HasColumnType("int")
                .IsRequired()
                .HasColumnName("RentalId");

            builder.Property(c => c.Emission)
                .HasColumnType("datetime")
                .IsRequired()
                .HasColumnName("Emission");

            builder.Property(c => c.Observation)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Observation");

            builder.Property(c => c.RentValue)
                .HasColumnName("RentValue")
                .HasColumnType("decimal(18,2)");

            builder.HasOne(c => c.Rental)
                .WithMany()
                .HasForeignKey(c => c.RentalId);
        }
    }
}
