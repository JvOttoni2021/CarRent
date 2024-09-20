using CarRent.Application.Services;
using CarRent.Domain.Events;
using MediatR;

namespace CarRent.Application.Handlers
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
