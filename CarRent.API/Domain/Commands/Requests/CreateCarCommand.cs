using MediatR;

namespace CarRent.API.Domain.Commands.Requests
{
    public record CreateCarCommand(string Model, string CarMaker, int Year) : IRequest<int>;
}
