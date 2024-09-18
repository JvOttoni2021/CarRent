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

        public Car? GetAvailableCarById(int Id) => _context.Cars.Where(c => c.Id == Id && c.Available).FirstOrDefault();

        public async Task<bool> setCarUnavailable(int CarId)
        {
            Car? availableCar = GetAvailableCarById(CarId);

            if (availableCar is null)
            {
                return false;
            }

            availableCar.Available = false;
            _context.Cars.Update(availableCar);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
