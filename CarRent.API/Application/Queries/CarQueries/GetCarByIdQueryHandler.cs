using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using CarRent.API.Infraestructure.Persistence.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CarRent.API.Application.Queries.CarQueries
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, Car?>
    {
        private readonly ICarRepository _carRepository;

        public GetCarByIdQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car?> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            return _carRepository.GetCarById(request.Id);
        }
    }
}
