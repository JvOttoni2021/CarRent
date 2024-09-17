using CarRent.API.Application.Persistence;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;

namespace CarRent.API.Application.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentContext _context;
        public CarRepository(CarRentContext context) { 
            _context = context;
        }

        public IEnumerable<Car> GetCars()
        {
            return _context.Cars;
        }
    }
}
