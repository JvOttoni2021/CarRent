using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Queries.CustomerQueries
{
    public record GetCustomersQuery() : IRequest<IEnumerable<Customer>?>;
}
