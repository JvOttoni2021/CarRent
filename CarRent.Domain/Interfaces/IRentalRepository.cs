using CarRent.Domain.Entities;

namespace CarRent.Domain.Interfaces
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetRentals();
        public Rental? GetRentalById(int id);
        public Rental? GetUnfinishedRentalById(int id);
        public Task CreateRental(Rental rental);
        public Task Update(Rental rental);
    }
}
