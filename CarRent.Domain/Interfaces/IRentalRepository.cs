using CarRent.Domain.Entities;

namespace CarRent.Domain.Interfaces
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetRentals();
        public Rental? GetRentalById(int id);
        public Rental? GetUnfinishedRentalById(int id);
        public Task<Rental> ReturnCar(Rental rental);
        public Task<Rental> CreateRental(Car car, Customer customer, DateTime expectedRetunDate);
    }
}
