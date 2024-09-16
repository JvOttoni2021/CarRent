using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Values;

namespace CarRent.API.Domain.Entities
{
    public class PaymentReceipt
    {
        public int Id { get; set; }
        public Rental Rental { get; set; }
        public Money RentValue { get; set; }
        public Money Fee { get; set; }
        public DateTime Emission { get; private set; }

    }
}
