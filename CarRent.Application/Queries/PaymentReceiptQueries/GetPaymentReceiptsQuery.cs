using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Queries.PaymentReceiptQueries
{
    public record GetPaymentReceiptsQuery() : IRequest<IEnumerable<PaymentReceipt>?>;
}
