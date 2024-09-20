using Azure.Core;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using CarRent.API.Infraestructure.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CarRent.API.Infraestructure.Persistence.Repositories
{
    public class RentalRepository : IRentalRepository
    {

        private readonly CarRentContext _context;
        public RentalRepository(CarRentContext context)
        {
            _context = context;
        }

        public Rental? GetRentalById(int Id)
        {
            return _context.Rentals.Where(c => c.Id == Id).FirstOrDefault();
        }

        public Rental? GetUnfinishedRentalById(int Id)
        {
            return _context.Rentals.Where(c => c.Id == Id && !c.CarReturned).FirstOrDefault();
        }

        public IEnumerable<Rental> GetRentals()
        {
            return _context.Rentals.ToArray();
        }

        public async Task<Rental> CreateRental(Car car, Customer customer, DateTime expectedReturnDate)
        {
            Console.WriteLine($"Criando locação para carro {car.Id}.");
            var rental = new Rental
            {
                RentedCar = car,
                Customer = customer,
                ExpectedReturnDate = expectedReturnDate,
                RentalDate = DateTime.Now,
                ReturnDate = null
            };

            _context.Entry(car).State = EntityState.Unchanged;
            _context.Entry(customer).State = EntityState.Unchanged;

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Locação {rental.Id} criado.");

            return rental;
        }

        public async Task<Rental> ReturnCar(Rental rental)
        {
            rental.CarReturned = true;
            rental.ReturnDate = DateTime.Now;

            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();

            return rental;
        }
    }
}
