using CarRent.API.Application.Persistence;
using CarRent.API.Domain.Commands.Responses;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Events;
using CarRent.API.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.API.Domain.Commands.Requests.RentalCommands
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Rental?>
    {
        private readonly CarRentContext _context;
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;

        public CreateRentalCommandHandler(CarRentContext context, ICarRepository carRepository, ICustomerRepository customerRepository, IMediator mediator)
        {
            _context = context;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public async Task<Rental?> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            Car? car = _carRepository.GetAvailableCarById(request.CarId);
            Customer? customer = _customerRepository.GetCustomerById(request.CustomerId);

            if (car is null || customer is null)
            {
                return null;
            }

            var rental = new Rental
            {
                RentedCar = car,
                Customer = customer,
                ExpectedReturnDate = request.ExpectedReturnDate,
                RentalDate = DateTime.Now
            };

            _context.Entry(car).State = EntityState.Unchanged;
            _context.Entry(customer).State = EntityState.Unchanged;

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            await _mediator.Publish(new RentalCreatedEvent(rental.Id));

            return rental;
        }
    }
}
