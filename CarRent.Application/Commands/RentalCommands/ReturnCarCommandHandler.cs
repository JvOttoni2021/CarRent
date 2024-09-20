using CarRent.Domain.Entities;
using CarRent.Domain.Events;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Commands.RentalCommands
{
    public class ReturnCarCommandHandler : IRequestHandler<ReturnCarCommand, Rental?>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMediator _mediator;

        public ReturnCarCommandHandler(IRentalRepository rentalRepository, IMediator mediator)
        {
            _rentalRepository = rentalRepository;
            _mediator = mediator;
        }
        public async Task<Rental?> Handle(ReturnCarCommand request, CancellationToken cancellationToken)
        {
            Rental? rental = _rentalRepository.GetRentalById(request.RentalId);

            if (rental is null)
            {
                return null;
            }

            await _rentalRepository.ReturnCar(rental);

            await _mediator.Publish(new CarReturnedEvent(rental));

            return rental;
        }
    }

}
