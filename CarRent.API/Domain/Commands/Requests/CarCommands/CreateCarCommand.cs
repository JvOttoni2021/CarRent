using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Domain.Commands.Requests.CarCommands
{
    public record CreateCarCommand(string Model, string Maker, int Year) : IRequest<Car> { }
}
