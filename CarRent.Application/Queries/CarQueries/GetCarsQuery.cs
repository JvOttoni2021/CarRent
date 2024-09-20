using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Queries.CarQueries
{
    public record GetCarsQuery() : IRequest<IEnumerable<Car>?>;
}
