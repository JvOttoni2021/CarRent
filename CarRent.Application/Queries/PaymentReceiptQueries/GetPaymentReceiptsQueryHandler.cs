using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Queries.PaymentReceiptQueries
{
    public class GetPaymentReceiptsQueryHandler : IRequestHandler<GetPaymentReceiptsQuery, IEnumerable<PaymentReceipt>?>
    {
        private readonly IPaymentReceiptRepository _paymentReceiptRepository;

        public GetPaymentReceiptsQueryHandler(IPaymentReceiptRepository paymentReceiptRepository)
        {
            _paymentReceiptRepository = paymentReceiptRepository;
        }

        public async Task<IEnumerable<PaymentReceipt>?> Handle(GetPaymentReceiptsQuery request, CancellationToken cancellationToken)
        {
            return _paymentReceiptRepository.GetPaymentReceipts();
        }
    }
}
