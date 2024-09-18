using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;

namespace CarRent.API.Domain.Service
{
    public class RentService
    {
        private readonly ICarRepository _carRepository;
        private readonly IRentalRepository _rentalRepository;

        public RentService(ICarRepository carRepository, IRentalRepository rentalRepository)
        {
            _carRepository = carRepository;
            _rentalRepository = rentalRepository;
        }


        public Task ProcessRentalCreation(int rentalId)
        {
            Rental? rental = _rentalRepository.GetRentalById(rentalId);
            _ = _carRepository.setCarUnavailable(rental.RentedCar.Id);

            return Task.CompletedTask;
        }
    }
}
