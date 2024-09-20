using CarRent.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.Queries.PaymentReceiptQueries
{
    public record GetPaymentReceiptByIdQuery(int Id) : IRequest<PaymentReceipt?>;
}
