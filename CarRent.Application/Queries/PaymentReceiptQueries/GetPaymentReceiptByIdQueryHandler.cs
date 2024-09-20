using CarRent.Application.Queries.CustomerQueries;
using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Queries.PaymentReceiptQueries
{
    public class GetPaymentReceiptByIdQueryHandler : IRequestHandler<GetPaymentReceiptByIdQuery, PaymentReceipt?>
    {
        private readonly IPaymentReceiptRepository _paymentReceiptRepository;

        public GetPaymentReceiptByIdQueryHandler(IPaymentReceiptRepository paymentReceiptRepository)
        {
            _paymentReceiptRepository = paymentReceiptRepository;
        }

        public async Task<PaymentReceipt?> Handle(GetPaymentReceiptByIdQuery request, CancellationToken cancellationToken)
        {
            return _paymentReceiptRepository.GetPaymentById(request.Id);
        }
    }
}
