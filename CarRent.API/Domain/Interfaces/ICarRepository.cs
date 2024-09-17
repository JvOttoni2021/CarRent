using CarRent.API.Domain.Entity;

namespace CarRent.API.Domain.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars();
    }
}
