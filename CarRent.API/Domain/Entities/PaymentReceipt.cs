using CarRent.API.Domain.Entity;

namespace CarRent.API.Domain.Entities
{
    public class PaymentReceipt
    {
        public int Id { get; set; }
        public virtual required Rental Rental { get; set; }
        public decimal RentValue { get; set; }
        public required string Observation { get; set; }
        public DateTime Emission { get; set; }

    }
}
