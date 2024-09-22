using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Commands.RentalCommands
{
    public record ReturnCarCommand(int Id) : IRequest<Rental?> { }
}
