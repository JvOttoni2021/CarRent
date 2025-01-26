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
        private readonly ILogger<RentalRepository> _logger;
        public RentalRepository(CarRentContext context, ILogger<RentalRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SaveChangesAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }

        public Rental? GetRentalById(int Id)
        {
            return _context.Rentals.Where(c => c.Id == Id).FirstOrDefault();
        }

        public Rental? GetUnfinishedRentalById(int Id)
        {
            return _context.Rentals.Where(c => c.Id == Id && !c.CarReturned).FirstOrDefault();
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

        public async Task<Rental> ReturnCar(Rental rental)
        {
            rental.CarReturned = true;
            rental.ReturnDate = DateTime.Now;

            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();

            return rental;
        }

        public async Task<int> UpdateRentalDatesById(int RentalId, DateTime RentalDate, DateTime ExpectedReturnDate)
        {
            Rental updatedRental = GetRentalById(RentalId);

            updatedRental.RentalDate = RentalDate;
            updatedRental.ExpectedReturnDate = ExpectedReturnDate;

            _context.Rentals.Update(updatedRental);

            await _context.SaveChangesAsync();

            return updatedRental.Id;
        }
    }
}
