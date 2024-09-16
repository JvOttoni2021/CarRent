using CarRent.API.Domain.Values;

namespace CarRent.API.Domain.Entity
{
    public class Rental
    {
        public int Id { get; private set; }
        public Car RentedCar { get; private set; }
        public Customer Customer { get; private set; }
        public DateTime RentalDate { get; private set; }
        public DateTime ExpectedReturnDate { get; private set; }
        public bool CarReturned { get; private set; }
    }
}
