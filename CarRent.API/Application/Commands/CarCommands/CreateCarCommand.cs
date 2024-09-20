using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Application.Commands.CarCommands
{
    public record CreateCarCommand(string Model, string Maker, int Year, decimal DailyPrice) : IRequest<Car> { }
}
