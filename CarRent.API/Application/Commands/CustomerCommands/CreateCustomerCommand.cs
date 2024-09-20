using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Application.Commands.CustomerCommands
{
    public record CreateCustomerCommand(string Name, string Cpf) : IRequest<Customer> { };
}
