using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Application.Queries.CustomerQueries
{
    public record GetCustomersQuery() : IRequest<IEnumerable<Customer>?>;
}
