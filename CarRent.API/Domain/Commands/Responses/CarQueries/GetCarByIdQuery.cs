using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Domain.Commands.Responses.CarQueries
{
    public record GetCarByIdQuery(int Id) : IRequest<Car?>;
}
