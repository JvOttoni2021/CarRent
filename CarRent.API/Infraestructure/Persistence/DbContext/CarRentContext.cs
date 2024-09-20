using CarRent.API.Domain.Entities;
using CarRent.API.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.API.Infraestructure.Persistence.Persistence
{
    public class CarRentContext : IdentityDbContext<UserAccess, RoleAccess, int>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PaymentReceipt> PaymentReceipts { get; set; }

        public CarRentContext(DbContextOptions<CarRentContext> options) : base(options)
        {
        }
    }
}
