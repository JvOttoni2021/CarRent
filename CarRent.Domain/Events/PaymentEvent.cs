using System.Diagnostics.CodeAnalysis;
using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Domain.Events
{
    [ExcludeFromCodeCoverage(Justification = "Event without logic to test")]
    public class PaymentEvent : INotification
    {
        public Rental rental { get; }

        public PaymentEvent(Rental rental)
        {
            this.rental = rental;
        }
    }
}
