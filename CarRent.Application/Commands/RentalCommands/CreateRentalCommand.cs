using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Commands.RentalCommands
{
    public record CreateRentalCommand(int CarId, int CustomerId, DateTime ExpectedReturnDate) : IRequest<Rental?> { }
}
