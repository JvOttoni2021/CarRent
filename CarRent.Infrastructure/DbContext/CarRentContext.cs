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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
