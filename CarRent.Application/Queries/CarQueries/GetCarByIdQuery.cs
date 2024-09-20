using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Queries.CarQueries
{
    public record GetCarByIdQuery(int Id) : IRequest<Car?>;
}
