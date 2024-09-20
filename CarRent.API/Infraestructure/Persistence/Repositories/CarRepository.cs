using Azure.Core;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using CarRent.API.Infraestructure.Persistence.Persistence;

namespace CarRent.API.Infraestructure.Persistence.Repositories
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

        public Car? GetCarByIdAvailability(int id, bool availability) => _context.Cars.Where(c => c.Id == id && c.Available == availability).FirstOrDefault();

        public async Task<bool> setCarAvailability(int carId, bool availability)
        {
            Car? availableCar = GetCarByIdAvailability(carId, !availability);

            if (availableCar is null)
            {
                return false;
            }

            availableCar.Available = availability;
            _context.Cars.Update(availableCar);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Car> CreateNewCar(string model, string maker, decimal dailyPrice, int year)
        {
            var newCar = new Car
            {
                Model = model,
                Maker = maker,
                DailyPrice = dailyPrice,
                Year = year
            };

            _context.Cars.Add(newCar);
            await _context.SaveChangesAsync();

            return newCar;
        }
    }
}
