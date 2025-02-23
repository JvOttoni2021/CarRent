using CarRent.Domain.Exceptions;

namespace CarRent.Domain.Entities
{
    public class PaymentReceipt
    {
        public int Id { get; }
        public virtual int RentalId { get; }
        public virtual Rental? Rental { get; private set; }
        public decimal? RentValue { get; private set; }
        public string? Observation { get; init; }
        public DateTime Emission { get; init; }

        protected PaymentReceipt() { }

        public PaymentReceipt(Rental? rental, decimal? rentValue, string? observation)
        {
            Rental = rental;
            RentValue = rentValue;
            Observation = observation;
            Emission = DateTime.Now;

            Validate();
        }

        private void Validate()
        {
            if (Rental is null)
                throw new DomainException($"{nameof(Rental)} não pode ser nulo.");

            if (RentValue is null || RentValue <= 0)
                throw new DomainException($"{nameof(RentValue)} deve possuir um valor positivo.");
            
            if (string.IsNullOrEmpty(Observation))
                throw new DomainException($"{nameof(Observation)} não pode ser vazio.");
        }
    }
}
