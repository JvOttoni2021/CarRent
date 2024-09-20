using CarRent.API.Application.Services;
using CarRent.API.Domain.Events;
using MediatR;

namespace CarRent.API.Application.EventHandlers
{
    public class CarReturnedEventHandler : INotificationHandler<CarReturnedEvent>
    {
        private readonly CarReturnedService _service;

        public CarReturnedEventHandler(CarReturnedService service)
        {
            _service = service;
        }

        public async Task Handle(CarReturnedEvent notification, CancellationToken cancellationToken)
        {
            await _service.ProcessCarReturn(notification.Rental);
        }
    }
}
