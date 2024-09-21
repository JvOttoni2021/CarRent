using CarRent.Domain.Entities;
using CarRent.Domain.Events;
using CarRent.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Services
{
    public class CarReturnedService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CarReturnedService(ICarRepository carRepository, IMediator mediator, ILogger logger)
        {
            _carRepository = carRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task ProcessCarReturn(Rental rental)
        {
            _logger.LogInformation($"{rental.Id} - Tornando carro disponível novamente.");
            await _carRepository.setCarAvailability(rental.RentedCar.Id, true);

            _logger.LogInformation($"{rental.Id} - Carro {rental.RentedCar.Id} agora está disponível.");
            await _mediator.Publish(new PaymentEvent(rental));
        }
    }
}
