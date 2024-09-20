using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Events;
using CarRent.API.Domain.Interfaces;
using MediatR;

namespace CarRent.API.Application.Services
{
    public class RentService
    {
        private readonly ICarRepository _carRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IMediator _mediator;

        public RentService(ICarRepository carRepository, IRentalRepository rentalRepository, IMediator mediator)
        {
            _carRepository = carRepository;
            _rentalRepository = rentalRepository;
            _mediator = mediator;
        }


        public async Task<Task> ProcessRentalCreation(int rentalId)
        {
            Console.WriteLine($"{rentalId} - Reservando carro para aluguel {rentalId}.");


            Rental? rental = _rentalRepository.GetRentalById(rentalId);

            await _mediator.Publish(new PaymentEvent(rental));

            _ = _carRepository.setCarAvailability(rental.RentedCar.Id, false);

            Console.WriteLine($"{rentalId} - Carro {rental.RentedCar.Id} reservado.");
            return Task.CompletedTask;
        }
    }
}
