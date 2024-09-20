using CarRent.Domain.Entities;
using CarRent.Domain.Events;
using CarRent.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Services
{
    public class RentService
    {
        private readonly ICarRepository _carRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<RentService> _logger;

        public RentService(ICarRepository carRepository, IRentalRepository rentalRepository, IMediator mediator, ILogger<RentService> logger)
        {
            _carRepository = carRepository;
            _rentalRepository = rentalRepository;
            _mediator = mediator;
            _logger = logger;
        }


        public async Task<Task> ProcessRentalCreation(int rentalId)
        {
            _logger.LogInformation($"{rentalId} - Reservando carro para locação {rentalId}.");


            Rental? rental = _rentalRepository.GetRentalById(rentalId);

            await _mediator.Publish(new PaymentEvent(rental));

            _ = _carRepository.setCarAvailability(rental.RentedCar.Id, false);

            _logger.LogInformation($"{rentalId} - Carro {rental.RentedCar.Id} reservado.");
            return Task.CompletedTask;
        }
    }
}
