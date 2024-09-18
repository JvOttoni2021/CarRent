using CarRent.API.Application.Persistence;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;

namespace CarRent.API.Application.Repositories
{
    public class RentalRepository : IRentalRepository
    {

        private readonly CarRentContext _context;
        public RentalRepository(CarRentContext context)
        {
            _context = context;
        }

        public Rental? GetRentalById(int Id)
        {
            return _context.Rentals.Where(c => c.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Rental> GetRentals()
        {
            return _context.Rentals.ToArray();
        }
    }
}
