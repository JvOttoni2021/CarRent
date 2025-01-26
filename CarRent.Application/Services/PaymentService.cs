using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Services
{
    public class PaymentService
    {
        private readonly IPaymentReceiptRepository _paymentReceiptRepository;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IPaymentReceiptRepository paymentReceiptRepository, ILogger<PaymentService> logger)
        {
            _paymentReceiptRepository = paymentReceiptRepository;
            _logger = logger;
        }

        public async Task ProcessPayment(Rental rental)
        {
            _logger.LogInformation("{RentalId} - Início de verificação de cobrança...", rental.Id);

            (decimal? price, string? description) result;
            // Lógica para verificar se é a retirada ou devolução do automovel
            if (!rental.CarReturned)
            {
                result = CalculateRent(rental);
            }
            else
            {
                result = CalculateFees(rental);

                if (result.price is null)
                {
                    _logger.LogInformation("{RentalId} - Nenhuma cobrança adicional necessária.", rental.Id);
                    return;
                }
            }
            PaymentReceipt paymentReceipt = new PaymentReceipt(rental, result.price, result.description);
            await _paymentReceiptRepository.CreatePaymentReceipt(paymentReceipt);

            _logger.LogInformation("{RentalId} - Cobrança '{PaymentDescription}' gerada.", rental.Id, paymentReceipt.Observation);
        }

        private static (decimal? price, string? description) CalculateRent(Rental rental)
        {
            DateTime rentalDate = rental.RentalDate;
            DateTime expectedReturnDate = rental.ExpectedReturnDate;

            int differenceInDays = (expectedReturnDate.Date - rentalDate.Date).Days;

            decimal? finalPrice = differenceInDays * rental.RentedCar!.DailyPrice;

            string? paymentDescription = "Pagamento inicial (Retirada).";

            return (finalPrice, paymentDescription);
        }

        private static (decimal? price, string? description) CalculateFees(Rental rental)
        {
            // Lógica para pagamento de juros de devolução atrasada
            DateTime expectedReturnDate = rental.ExpectedReturnDate;
            DateTime realReturnDate = DateTime.Now;

            bool lateReturn = realReturnDate > expectedReturnDate;

            if (!lateReturn)
            {
                return (null, null);
            }

            int differenceInDays = (realReturnDate.Date - expectedReturnDate.Date).Days;

            // Cobrança normal + 10%
            decimal? finalPrice = differenceInDays * rental.RentedCar!.DailyPrice * 1.10m;

            string? paymentDescription = "Pagamento de juros (Devolução atrasada).";

            return (finalPrice, paymentDescription);
        }
    }
}
