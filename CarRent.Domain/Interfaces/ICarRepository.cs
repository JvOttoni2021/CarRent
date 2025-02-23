using System.Diagnostics.CodeAnalysis;
using CarRent.Domain.Entities;

namespace CarRent.Domain.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars();
        public Car? GetCarByIdAvailability(int id, bool availability);
        public Car? GetCarById(int id);
        public Task UpdateCar(Car car);
        public Task CreateCar(Car car);
    }
}
