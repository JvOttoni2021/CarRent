using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Commands.RentalCommands
{
    public record ReturnCarCommand(int RentalId) : IRequest<Rental?> { }
}
