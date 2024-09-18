using CarRent.API.Domain.Entity;

namespace CarRent.API.Domain.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars();
        public Car? GetAvailableCarById(int Id);
        public Task<bool> setCarUnavailable(int Id);
    }
}
