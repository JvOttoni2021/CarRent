using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using CarRent.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRent.Infraestructure.Repositories
{
    public class PaymentReceiptRepository : IPaymentReceiptRepository
    {
        private readonly CarRentContext _context;

        public PaymentReceiptRepository(CarRentContext context)
        {
            _context = context;
        }

        public async Task CreatePaymentReceipt(PaymentReceipt paymentReceipt)
        {
            _context.PaymentReceipts.Add(paymentReceipt);
            await _context.SaveChangesAsync();
        }

        public PaymentReceipt? GetPaymentById(int id)
        {
            return _context.PaymentReceipts.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<PaymentReceipt> GetPaymentReceipts()
        {
            return _context.PaymentReceipts.ToArray();
        }

        public IEnumerable<PaymentReceipt> GetPaymentReceiptsByRentalId(int RentalId)
        {
            return _context.PaymentReceipts.Where(c => c.Rental!.Id == RentalId).ToArray();
        }
    }
}
