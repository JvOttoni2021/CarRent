using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using MediatR;

namespace CarRent.API.Application.Queries.RentalQueries
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
