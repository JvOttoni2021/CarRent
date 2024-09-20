using CarRent.Application.Queries.PaymentReceiptQueries;
using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.Queries.RentalQueries
{
    public class GetRentalByIdQueryHandler : IRequestHandler<GetRentalByIdQuery, Rental?>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetRentalByIdQueryHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<Rental?> Handle(GetRentalByIdQuery request, CancellationToken cancellationToken)
        {
            return _rentalRepository.GetRentalById(request.Id);
        }
    }
}
