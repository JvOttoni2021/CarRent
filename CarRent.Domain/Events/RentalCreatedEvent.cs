using MediatR;

namespace CarRent.Domain.Events
{
    public class RentalCreatedEvent : INotification
    {
        public int RentalId { get; }

        public RentalCreatedEvent(int rentalId)
        {
            RentalId = rentalId;
        }
    }
}
