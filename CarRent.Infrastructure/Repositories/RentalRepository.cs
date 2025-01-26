using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using CarRent.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace CarRent.Infraestructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {

        private readonly CarRentContext _context;
        public RentalRepository(CarRentContext context)
        {
            _context = context;
        }

        public async Task Update(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }

        public Rental? GetRentalById(int id)
        {
            return _context.Rentals.Where(c => c.Id == id).FirstOrDefault();
        }

        public Rental? GetUnfinishedRentalById(int id)
        {
            return _context.Rentals.Where(c => c.Id == id && !c.CarReturned).FirstOrDefault();
        }

        public IEnumerable<Rental> GetRentals()
        {
            return _context.Rentals.ToArray();
        }

        public async Task CreateRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
        }
    }
}
