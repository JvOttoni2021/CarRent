using CarRent.API.Domain.Entities;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using CarRent.API.Infraestructure.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarRent.API.Infraestructure.Persistence.Repositories
{
    public class PaymentReceiptRepository : IPaymentReceiptRepository
    {
        private readonly CarRentContext _context;

        public PaymentReceiptRepository(CarRentContext context)
        {
            _context = context;
        }

        public async Task CreatePaymentReceipt(Rental Rental, double RentValue, string Observation)
        {
            Console.WriteLine($"{Rental.Id} - Criando recibo para aluguel {Rental.Id}.");

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

            Console.WriteLine($"{Rental.Id} - Recibo {newPayment.Id} criado.");
        }

        public IEnumerable<PaymentReceipt> GetPaymentReceipts()
        {
            return _context.PaymentReceipts.ToArray();
        }
    }
}
