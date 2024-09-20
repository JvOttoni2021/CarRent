using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Services
{
    public class PaymentService
    {
        private readonly ICarRepository _carRepository;
        private readonly IPaymentReceiptRepository _paymentReceiptRepository;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(ICarRepository carRepository, IPaymentReceiptRepository paymentReceiptRepository, ILogger<PaymentService> logger)
        {
            _carRepository = carRepository;
            _paymentReceiptRepository = paymentReceiptRepository;
            _logger = logger;
        }

        public async Task ProcessPayment(Rental rental)
        {
            _logger.LogInformation($"{rental.Id} - Início de verificação de cobrança...");

            string paymentDescription = "";

            // Lógica para verificar se será cobrança inicial ou juros
            if (!rental.CarReturned)
            {
                // Lógica para retirada inicial

                DateTime rentalDate = rental.RentalDate;
                DateTime expectedReturnDate = rental.ExpectedReturnDate;

                int differenceInDays = (expectedReturnDate.Date - rentalDate.Date).Days;

                decimal finalPrice = differenceInDays * rental.RentedCar.DailyPrice;

                paymentDescription = "Pagamento inicial (Retirada).";

                await _paymentReceiptRepository.CreatePaymentReceipt(rental, finalPrice, paymentDescription);
            }
            else
            {
                // Lógica para pagamento de juros de devolução atrasada

                DateTime expectedReturnDate = rental.ExpectedReturnDate;
                DateTime realReturnDate = DateTime.Now;

                bool lateReturn = realReturnDate > expectedReturnDate;

                if (!lateReturn)
                {
                    _logger.LogInformation($"{rental.Id} - Nenhuma cobrança necessária.");
                    return;
                }

                int differenceInDays = (realReturnDate.Date - expectedReturnDate.Date).Days;

                // Cobrança normal + 10%
                decimal finalPrice = differenceInDays * rental.RentedCar.DailyPrice * 1.10m;

                paymentDescription = "Pagamento de juros (Devolução atrasada).";

                await _paymentReceiptRepository.CreatePaymentReceipt(rental, finalPrice, paymentDescription);
            }


            _logger.LogInformation($"{rental.Id} - Cobrança '{paymentDescription}' gerada para locação {rental.Id}.");
        }
    }
}
