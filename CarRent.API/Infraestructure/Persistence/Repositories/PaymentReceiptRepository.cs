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
            Console.WriteLine("Criando recibo.");
            Console.WriteLine($"RentalDate: {Rental.RentalDate}");
            Console.WriteLine($"ExpectedReturnDate: {Rental.ExpectedReturnDate}");
            PaymentReceipt newPayment = new PaymentReceipt
            {
                Rental = Rental,
                Observation = Observation,
                Emission = new DateTime(2024, 9, 18),
                RentValue = RentValue
            };

            _context.Entry(Rental).State = EntityState.Unchanged;
            _context.PaymentReceipts.Add(newPayment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        public IEnumerable<PaymentReceipt> GetPaymentReceipts()
        {
            return _context.PaymentReceipts.ToArray();
        }
    }
}
