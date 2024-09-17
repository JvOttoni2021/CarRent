using CarRent.API.Domain.Entity;

namespace CarRent.API.Domain.Entities
{
    public class PaymentReceipt
    {
        public int Id { get; set; }
        public Rental Rental { get; set; }
        public decimal RentValue { get; set; }
        public decimal Fee { get; set; }
        public DateTime Emission { get; private set; }

    }
}
