using CarRent.Domain.Entities;

namespace CarRent.Application.Dtos
{
    public class RentalDto
    {
        public int Id { get; set; }
        public virtual Car RentedCar { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool CarReturned { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
    }
}
