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
    public class RentalMapping : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("Rentals");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.IdRentedCar)
                .HasColumnName("RentedCarId")
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.IdCustomer)
                .HasColumnName("CustomerId")
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.RentalDate)
                .HasColumnName("RentalDate")
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.ExpectedReturnDate)
                .HasColumnName("ExpectedReturnDate")
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.CarReturned)
                .HasColumnName("CarReturned")
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.ReturnDate)
                .HasColumnName("ReturnDate")
                .HasColumnType("DateTime");

            builder.HasOne(c => c.RentedCar)
                .WithMany()
                .HasForeignKey(c => c.IdRentedCar);

            builder.HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.IdCustomer);
        }
    }
}
