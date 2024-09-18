using CarRent.API.Application.Persistence;
using CarRent.API.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CarRent.API.Domain.Commands.Responses.CarQueries
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, Car?>
    {
        private readonly CarRentContext _context;

        public GetCarByIdQueryHandler(CarRentContext context)
        {
            _context = context;
        }

        public async Task<Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car = await _context.Cars.FindAsync(request.Id);
            return car;
        }
    }
}
