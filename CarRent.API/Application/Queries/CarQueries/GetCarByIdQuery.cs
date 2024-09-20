using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Application.Queries.CarQueries
{
    public record GetCarByIdQuery(int Id) : IRequest<Car?>;
}
