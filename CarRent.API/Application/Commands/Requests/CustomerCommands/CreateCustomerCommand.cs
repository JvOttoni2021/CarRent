using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Application.Commands.Requests.CustomerCommands
{
    public record CreateCustomerCommand(string Name, string Cpf) : IRequest<Customer> { };
}
