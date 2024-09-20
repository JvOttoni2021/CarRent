using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Commands.CarCommands
{
    public record CreateCarCommand(string Model, string Maker, int Year, decimal DailyPrice) : IRequest<Car> { }
}
