using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using CarRent.API.Infraestructure.Persistence.Persistence;
using MediatR;

namespace CarRent.API.Application.Queries.CarQueries
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
