using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Commands.CustomerCommands
{
    public record CreateCustomerCommand(string Name, string Cpf) : IRequest<Customer> { };
}
