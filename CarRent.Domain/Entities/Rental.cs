using CarRent.Domain.Exceptions;
using System.Runtime.CompilerServices;

namespace CarRent.Domain.Entities
{
    public class Rental
    {
        public int Id { get; }
        public int IdRentedCar { get; }
        public int IdCustomer { get; }
        public virtual Car? RentedCar { get; init; }
        public virtual Customer? Customer { get; init; }
        public DateTime RentalDate { get; private set; } = DateTime.Now;
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ReturnDate { get; private set; } = null;
        public bool CarReturned { get; private set; } = false;

        protected Rental() { }

        public Rental(Car? car, Customer? customer, DateTime expectedReturnDate)
        {
            RentedCar = car;
            Customer = customer;
            ExpectedReturnDate = expectedReturnDate;

            IsValid();
        }

        public void ReturnCar()
        {
            ReturnDate = DateTime.Now;
            CarReturned = true;
        }

        private void IsValid()
        {
            if (RentedCar is null)
                throw new DomainException($"{nameof(RentedCar)} não pode ser nulo");

            if (Customer is null)
                throw new DomainException($"{nameof(Customer)} não pode ser nulo");

            if (DateTime.Now > ExpectedReturnDate)
                throw new DomainException($"{nameof(ExpectedReturnDate)} não pode estar no passado.");
        }
    }
}
