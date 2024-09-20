using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Queries.CarQueries
{
    public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, IEnumerable<Car>?>
    {
        private readonly ICarRepository _carRepository;

        public GetCarsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>?> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            return _carRepository.GetCars();
        }
    }
}
