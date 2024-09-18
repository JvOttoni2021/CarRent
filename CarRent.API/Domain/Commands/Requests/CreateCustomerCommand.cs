using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Domain.Commands.Requests
{
    public record CreateCustomerCommand(string Name, string Cpf) : IRequest<Customer> { };
}
