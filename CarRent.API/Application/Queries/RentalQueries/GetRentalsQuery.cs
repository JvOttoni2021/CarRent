using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Application.Queries.RentalQueries
{
    public record GetRentalsQuery() : IRequest<IEnumerable<Rental>?>;
}
