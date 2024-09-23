using CarRent.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.Queries.PaymentReceiptQueries
{
    public record GetPaymentReceiptByRentalIdQuery(int RentalId) : IRequest<IEnumerable<PaymentReceipt?>>;
}
