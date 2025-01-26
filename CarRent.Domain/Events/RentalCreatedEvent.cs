using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Domain.Events
{
    public class RentalCreatedEvent : INotification
    {
        public Rental Rental { get; }

        public RentalCreatedEvent(Rental rentalId)
        {
            Rental = rentalId;
        }
    }
}
