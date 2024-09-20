using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Queries.RentalQueries
{
    public class GetRentalsQueryHandler : IRequestHandler<GetRentalsQuery, IEnumerable<Rental>?>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetRentalsQueryHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<IEnumerable<Rental>?> Handle(GetRentalsQuery request, CancellationToken cancellationToken)
        {
            return _rentalRepository.GetRentals();
        }
    }
}
