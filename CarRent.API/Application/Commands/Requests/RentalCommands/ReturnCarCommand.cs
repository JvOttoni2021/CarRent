using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Application.Commands.Requests.RentalCommands
{
    public record ReturnCarCommand(int RentalId) : IRequest<Rental?> { }
}
