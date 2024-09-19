using CarRent.API.Application.Services;
using CarRent.API.Domain.Events;
using MediatR;

namespace CarRent.API.Application.EventHandlers
{
    public class PaymentEventHandler : INotificationHandler<PaymentEvent>
    {
        private readonly PaymentService _paymentService;

        public PaymentEventHandler(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(PaymentEvent notification, CancellationToken cancellationToken)
        {
            await _paymentService.ProcessPayment(notification.rental);
        }
    }
}
