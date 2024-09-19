namespace CarRent.API.Domain.Events
{
    public class CarReturnedEvent
    {
        public int RentalId { get; }

        public CarReturnedEvent(int rentalId)
        {
            this.RentalId = rentalId;
        }
    }
}
