using System.Diagnostics.CodeAnalysis;
using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Domain.Events
{
    [ExcludeFromCodeCoverage(Justification = "Event without logic to test")]
    public class CarReturnedEvent : INotification
    {
        public Rental Rental { get; }

        public CarReturnedEvent(Rental rental)
        {
            Rental = rental;
        }
    }
}
