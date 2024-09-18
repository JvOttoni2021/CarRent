
namespace CarRent.API.Domain.Entity
{
    public class Rental
    {
        public int Id { get; private set; }
        public virtual Car RentedCar { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public bool CarReturned { get; private set; }
    }
}
