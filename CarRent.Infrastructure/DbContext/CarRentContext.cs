using Microsoft.EntityFrameworkCore;
using CarRent.Domain.Entities;


namespace CarRent.Infrastructure.DbContext
{
    public class CarRentContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PaymentReceipt> PaymentReceipts { get; set; }

        public CarRentContext(DbContextOptions<CarRentContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(c => c.DailyPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PaymentReceipt>()
                .Property(p => p.RentValue)
                .HasColumnType("decimal(18,2)");
        }
    }
}
