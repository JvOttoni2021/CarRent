using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Queries.CarQueries
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, Car?>
    {
        private readonly IRentalRepository _carRepository;

        public GetCarByIdQueryHandler(IRentalRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car?> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            return _carRepository.GetCarById(request.Id);
        }
    }
}
