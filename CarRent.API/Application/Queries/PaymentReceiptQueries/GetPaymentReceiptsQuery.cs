using CarRent.API.Domain.Entities;
using MediatR;

namespace CarRent.API.Application.Queries.PaymentReceiptQueries
{
    public record GetPaymentReceiptsQuery() : IRequest<IEnumerable<PaymentReceipt>?>;
}
