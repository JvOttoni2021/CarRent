using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Queries.RentalQueries
{
    public record GetRentalsQuery() : IRequest<IEnumerable<Rental>?>;
}
