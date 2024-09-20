using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Domain.Events
{
    public class CarReturnedEvent : INotification
    {
        public Rental Rental { get; }

        public CarReturnedEvent(Rental rental)
        {
            this.Rental = rental;
        }
    }
}
