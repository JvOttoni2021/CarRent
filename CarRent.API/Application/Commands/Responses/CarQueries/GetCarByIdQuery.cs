using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Application.Commands.Responses.CarQueries
{
    public record GetCarByIdQuery(int Id) : IRequest<Car?>;
}
