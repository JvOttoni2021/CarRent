using System.Diagnostics.CodeAnalysis;
using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Domain.Events
{
    [ExcludeFromCodeCoverage(Justification = "Event without logic to test")]
    public class RentalCreatedEvent : INotification
    {
        public Rental Rental { get; }

        public RentalCreatedEvent(Rental rentalId)
        {
            Rental = rentalId;
        }
    }
}
