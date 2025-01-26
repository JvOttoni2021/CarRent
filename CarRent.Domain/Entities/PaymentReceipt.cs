using CarRent.Domain.Exceptions;

namespace CarRent.Domain.Entities
{
    public class PaymentReceipt
    {
        public int Id { get; set; }
        public virtual int RentalId { get; }
        public virtual Rental? Rental { get; set; }
        public decimal? RentValue { get; set; }
        public string? Observation { get; set; }
        public DateTime Emission { get; set; }

        protected PaymentReceipt() { }

        public PaymentReceipt(Rental? rental, decimal? rentValue, string? observation)
        {
            Rental = rental;
            RentValue = rentValue;
            Observation = observation;
            Emission = DateTime.Now;

            IsValid();
        }

        private void IsValid()
        {
            if (Rental is null)
                throw new DomainException($"{nameof(Rental)} não pode ser nulo.");

            if (RentValue is null || RentValue <= 0)
                throw new DomainException($"{nameof(RentValue)} deve possuir um valor positivo.");
        }
    }
}
