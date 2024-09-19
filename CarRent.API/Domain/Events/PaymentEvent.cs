using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Domain.Events
{
    public class PaymentEvent : INotification
    {
        public Rental rental { get; }

        public PaymentEvent(Rental rental)
        {
            this.rental = rental;
        }
    }
}
