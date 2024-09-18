using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Domain.Commands.Requests.CustomerCommands
{
    public record CreateCustomerCommand(string Name, string Cpf) : IRequest<Customer> { };
}
