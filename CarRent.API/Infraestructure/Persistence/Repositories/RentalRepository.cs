using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using CarRent.API.Infraestructure.Persistence.Persistence;

namespace CarRent.API.Infraestructure.Persistence.Repositories
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
