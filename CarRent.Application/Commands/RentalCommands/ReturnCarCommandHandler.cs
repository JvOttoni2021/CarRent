using CarRent.Domain.Entities;
using CarRent.Domain.Events;
using CarRent.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Commands.RentalCommands
{
    public class ReturnCarCommandHandler : IRequestHandler<ReturnCarCommand, Rental?>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<ReturnCarCommandHandler> _logger;

        public ReturnCarCommandHandler(IRentalRepository rentalRepository, IMediator mediator, ILogger<ReturnCarCommandHandler> logger)
        {
            _rentalRepository = rentalRepository;
            _mediator = mediator;
            _logger = logger;
        }
        public async Task<Rental?> Handle(ReturnCarCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requisição recebida - Devolução de automóvel");
            Rental? rental = _rentalRepository.GetUnfinishedRentalById(request.RentalId);

            if (rental is null)
                return null;

            rental.ReturnCar();

            await _rentalRepository.Update(rental);

            await _mediator.Publish(new CarReturnedEvent(rental));

            _logger.LogInformation("Requisição finalizada - Devolução de automóvel");
            return rental;
        }
    }

}
