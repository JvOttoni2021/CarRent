using CarRent.API.Application.Persistence;
using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Domain.Commands.Requests.CarCommands
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Car>
    {
        private readonly CarRentContext context;

        public CreateCarCommandHandler(CarRentContext context)
        {
            this.context = context;
        }

        public async Task<Car> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = new Car
            {
                Model = request.Model,
                Maker = request.Maker,
                Year = request.Year
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            return car;
        }
    }
}
