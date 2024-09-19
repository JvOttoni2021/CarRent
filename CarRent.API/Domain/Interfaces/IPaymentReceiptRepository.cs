using CarRent.API.Domain.Entities;
using CarRent.API.Domain.Entity;

namespace CarRent.API.Domain.Interfaces
{
    public interface IPaymentReceiptRepository
    {

        public IEnumerable<PaymentReceipt> GetPaymentReceipts();
        public Task CreatePaymentReceipt(Rental rental, double rentValue, string Observation);
    }
}
