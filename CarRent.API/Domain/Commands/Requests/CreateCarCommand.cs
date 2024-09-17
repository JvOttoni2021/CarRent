using MediatR;

namespace CarRent.API.Domain.Commands.Requests
{
    public record CreateCarCommand(string Model, string Maker, int Year) : IRequest<int>;
}
