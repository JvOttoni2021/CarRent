using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Application.Commands.Requests.RentalCommands
{
    public record CreateRentalCommand(int CarId, int CustomerId, DateTime ExpectedReturnDate) : IRequest<Rental?> { }
}
