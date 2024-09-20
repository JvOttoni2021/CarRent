using CarRent.Domain.Entities;
using CarRent.Domain.Events;
using CarRent.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Commands.RentalCommands
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Rental?>
    {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateRentalCommandHandler> _logger;

        public CreateRentalCommandHandler(ICarRepository carRepository, 
            ICustomerRepository customerRepository, 
            IRentalRepository rentalRepository, 
            IMediator mediator,
            ILogger<CreateRentalCommandHandler> logger)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Rental?> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requisição recebida - Criar locação");

            Car? car = _carRepository.GetCarByIdAvailability(request.CarId, true);
            Customer? customer = _customerRepository.GetCustomerById(request.CustomerId);

            if (car is null || customer is null)
            {
                return null;
            }

            Rental newRental = await _rentalRepository.CreateRental(car, customer, request.ExpectedReturnDate);

            await _mediator.Publish(new RentalCreatedEvent(newRental.Id));

            _logger.LogInformation("Requisição finalizada - Criar locação");
            return newRental;
        }
    }
}
