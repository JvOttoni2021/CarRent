using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.Queries.PaymentReceiptQueries
{
    public class GetPaymentReceiptByRentalIdQueryHandler : IRequestHandler<GetPaymentReceiptByRentalIdQuery, IEnumerable<PaymentReceipt?>>
    {
        private readonly IPaymentReceiptRepository _paymentReceiptRepository;

        public GetPaymentReceiptByRentalIdQueryHandler(IPaymentReceiptRepository paymentReceiptRepository)
        {
            _paymentReceiptRepository = paymentReceiptRepository;
        }

        public async Task<IEnumerable<PaymentReceipt?>> Handle(GetPaymentReceiptByRentalIdQuery request, CancellationToken cancellationToken)
        {
            return _paymentReceiptRepository.GetPaymentReceiptsByRentalId(request.RentalId);
        }
    }
}
