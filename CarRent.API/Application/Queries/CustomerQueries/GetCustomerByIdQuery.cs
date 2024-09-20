using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Application.Queries.CustomerQueries
{
    public record GetCustomerByIdQuery(int Id) : IRequest<Customer?>;
}
