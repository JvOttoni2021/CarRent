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
        private readonly ILogger<PaymentReceiptRepository> _logger;

        public PaymentReceiptRepository(CarRentContext context, ILogger<PaymentReceiptRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreatePaymentReceipt(Rental Rental, decimal RentValue, string Observation)
        {
            _logger.LogInformation($"{Rental.Id} - Criando recibo para locação {Rental.Id}.");

            PaymentReceipt newPayment = new PaymentReceipt
            {
                Rental = Rental,
                Observation = Observation,
                Emission = DateTime.Now,
                RentValue = RentValue
            };

            _context.Entry(Rental).State = EntityState.Unchanged;
            _context.PaymentReceipts.Add(newPayment);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"{Rental.Id} - Recibo {newPayment.Id} criado.");
        }

        public PaymentReceipt? GetPaymentById(int id)
        {
            return _context.PaymentReceipts.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<PaymentReceipt> GetPaymentReceipts()
        {
            return _context.PaymentReceipts.ToArray();
        }
    }
}
