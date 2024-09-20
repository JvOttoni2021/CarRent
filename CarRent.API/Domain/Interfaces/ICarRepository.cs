using Azure.Core;
using CarRent.API.Domain.Entity;

namespace CarRent.API.Domain.Interfaces
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
