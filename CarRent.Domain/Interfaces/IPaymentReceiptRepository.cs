using CarRent.Domain.Entities;

namespace CarRent.Domain.Interfaces
{
    public interface IPaymentReceiptRepository
    {

        public IEnumerable<PaymentReceipt> GetPaymentReceipts();
        public PaymentReceipt? GetPaymentById(int id);
        public Task CreatePaymentReceipt(Rental rental, decimal rentValue, string observation);
    }
}
