namespace CarRent.Domain.Entities
{
    public class PaymentReceipt
    {
        public int Id { get; set; }
        public virtual Rental Rental { get; set; }
        public decimal RentValue { get; set; }
        public string Observation { get; set; }
        public DateTime Emission { get; set; }

    }
}
