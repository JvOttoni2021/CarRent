using CarRent.Domain.Entities;

namespace CarRent.Domain.Interfaces
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetRentals();
        public Rental? GetRentalById(int id);
        public Rental? GetUnfinishedRentalById(int id);
        public Task<Rental> ReturnCar(Rental rental);
        public Task CreateRental(Rental rental);
        public Task<int> UpdateRentalDatesById(int RentalId, DateTime RentalDate, DateTime ExpectedReturnDate);
        public Task SaveChangesAsync(Rental rental);
    }
}
