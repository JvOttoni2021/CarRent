using CarRent.API.Domain.Entity;

namespace CarRent.API.Domain.Interfaces
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetRentals();
        public Rental? GetRentalById(int Id);
    }
}
