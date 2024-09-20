using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Domain.Events
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
