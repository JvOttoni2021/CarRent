using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Events;
using CarRent.API.Domain.Interfaces;
using CarRent.API.Infraestructure.Persistence.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.API.Application.Commands.RentalCommands
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Rental?>
    {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IMediator _mediator;

        public CreateRentalCommandHandler(ICarRepository carRepository, ICustomerRepository customerRepository, IRentalRepository rentalRepository, IMediator mediator)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
            _mediator = mediator;
        }

        public async Task<Rental?> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Requisição recebida - Criar locação");

            Car? car = _carRepository.GetCarByIdAvailability(request.CarId, true);
            Customer? customer = _customerRepository.GetCustomerById(request.CustomerId);

            if (car is null || customer is null)
            {
                return null;
            }

            Rental newRental = await _rentalRepository.CreateRental(car, customer, request.ExpectedReturnDate);

            await _mediator.Publish(new RentalCreatedEvent(newRental.Id));

            Console.WriteLine("Requisição finalizada - Criar locação");
            return newRental;
        }
    }
}
