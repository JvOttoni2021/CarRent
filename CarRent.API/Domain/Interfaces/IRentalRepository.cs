using CarRent.API.Domain.Entity;

namespace CarRent.API.Domain.Interfaces
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetRentals();
        public Rental? GetRentalById(int id);
        public Task<Rental> ReturnCar(Rental rental);
        public Task<Rental> CreateRental(Car car, Customer customer, DateTime expectedRetunDate);
    }
}
