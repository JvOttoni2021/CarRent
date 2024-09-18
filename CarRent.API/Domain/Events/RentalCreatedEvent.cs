using MediatR;

namespace CarRent.API.Domain.Events
{
    public class RentalCreatedEvent : INotification
    {
        public int RentalId { get; }

        public RentalCreatedEvent(int rentalId)
        {
            this.RentalId = rentalId;
        }
    }
}
