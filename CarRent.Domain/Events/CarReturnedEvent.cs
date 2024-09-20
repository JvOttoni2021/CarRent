using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Domain.Events
{
    public class CarReturnedEvent : INotification
    {
        public Rental Rental { get; }

        public CarReturnedEvent(Rental rental)
        {
            Rental = rental;
        }
    }
}
