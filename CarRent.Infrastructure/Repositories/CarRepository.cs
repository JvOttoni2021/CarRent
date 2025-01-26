using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using CarRent.Infrastructure.DbContext;

namespace CarRent.Infraestructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentContext _context;
        public CarRepository(CarRentContext context)
        {
            _context = context;
        }

        public IEnumerable<Car> GetCars()
        {
            return _context.Cars.ToArray();
        }

        public Car? GetCarById(int id) => _context.Cars.Where(c => c.Id == id).FirstOrDefault();

        public Car? GetCarByIdAvailability(int id, bool availability) => _context.Cars.Where(c => c.Id == id && c.Available == availability).FirstOrDefault();

        public async Task CreateCar(Car car) {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCar(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }
    }
}
