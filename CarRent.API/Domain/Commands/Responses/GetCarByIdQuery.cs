using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Domain.Commands.Responses
{
    public record GetCarByIdQuery(int Id) : IRequest<Car?>;
}
