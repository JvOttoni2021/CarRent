using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;

namespace CarRent.API.Application.Services
{
    public class PaymentService
    {
        private readonly ICarRepository _carRepository;
        private readonly IPaymentReceiptRepository _paymentReceiptRepository;

        public PaymentService(ICarRepository carRepository, IPaymentReceiptRepository paymentReceiptRepository)
        {
            _carRepository = carRepository;
            _paymentReceiptRepository = paymentReceiptRepository;
        }

        public Task ProcessPayment(Rental rental)
        {
            // Lógica para verificar se será cobrança normal de contratação ou juros
            DateTime begin = rental.CarReturned ? rental.ExpectedReturnDate : rental.RentalDate;
            DateTime end = rental.CarReturned ? DateTime.Now : rental.ExpectedReturnDate;

            int differenceInDays = (end - begin).Days;
            double finalPrice = differenceInDays * rental.RentedCar.DailyPrice * (rental.CarReturned ? 1.1d : 1.0d);

            string paymentDescription = rental.CarReturned ? "Pagamento de juros de devolução." : "Pagamento inicial (Retirada).";

            _paymentReceiptRepository.CreatePaymentReceipt(rental, finalPrice, paymentDescription);

            return Task.CompletedTask;
        }
    }
}
