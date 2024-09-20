using CarRent.Domain.Entities;

namespace CarRent.Domain.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars();
        public Car? GetCarByIdAvailability(int id, bool availability);
        public Car? GetCarById(int id);
        public Task<bool> setCarAvailability(int id, bool availability);
        public Task<Car> CreateNewCar(string model, string maker, decimal dailyPrice, int year);
        public Task<Car?> UpdateCar(int id, string model, string maker, decimal dailyPrice, int year);
    }
}
