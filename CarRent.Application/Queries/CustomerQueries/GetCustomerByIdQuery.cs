using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Queries.CustomerQueries
{
    public record GetCustomerByIdQuery(int Id) : IRequest<Customer?>;
}
