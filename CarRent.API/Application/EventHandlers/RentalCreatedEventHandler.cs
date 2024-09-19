using CarRent.API.Application.Services;
using CarRent.API.Domain.Events;
using MediatR;

namespace CarRent.API.Application.Handlers
{
    public class RentalCreatedEventHandler : INotificationHandler<RentalCreatedEvent>
    {
        private readonly RentService _rentService;

        public RentalCreatedEventHandler(RentService rentService)
        {
            _rentService = rentService;
        }

        public async Task Handle(RentalCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _rentService.ProcessRentalCreation(notification.RentalId);
        }
    }
}
