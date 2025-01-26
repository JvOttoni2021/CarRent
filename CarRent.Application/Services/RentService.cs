using CarRent.Domain.Entities;
using CarRent.Domain.Events;
using CarRent.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Services
{
    public class RentService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<RentService> _logger;

        public RentService(IRentalRepository rentalRepository, IMediator mediator, ILogger<RentService> logger)
        {
            _rentalRepository = rentalRepository;
            _mediator = mediator;
            _logger = logger;
        }


        public async Task<Task> ProcessRentalCreation(int rentalId)
        {
            _logger.LogInformation("{RentalId} - Reservando carro para locação.", rentalId);

            Rental? rental = _rentalRepository.GetRentalById(rentalId);

            if (rental == null) 
                throw new ArgumentNullException(nameof(rentalId), $"Rental {rentalId} não encontrada.");

            await _mediator.Publish(new PaymentEvent(rental!));

            rental.RentedCar.ChangeAvailability(false);

            _logger.LogInformation("{RentalId} - Carro {CarId} reservado.", rentalId, rental.RentedCar.Id);
            return Task.CompletedTask;
        }
    }
}
