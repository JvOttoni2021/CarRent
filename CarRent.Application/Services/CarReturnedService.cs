using CarRent.Domain.Entities;
using CarRent.Domain.Events;
using CarRent.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Services
{
    public class CarReturnedService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<CarReturnedService> _logger;

        public CarReturnedService(IRentalRepository rentalRepository, IMediator mediator, ILogger<CarReturnedService> logger)
        {
            _rentalRepository = rentalRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task ProcessCarReturn(Rental rental)
        {
            _logger.LogInformation("{Rental} - Tornando carro disponível novamente.", rental.Id);

            rental.RentedCar!.ChangeAvailability(true);
            await _rentalRepository.Update(rental);

            _logger.LogInformation("{Rental} - Carro {RentedCar} agora está disponível.", rental.Id, rental.RentedCar.Id);
            await _mediator.Publish(new PaymentEvent(rental));
        }
    }
}
