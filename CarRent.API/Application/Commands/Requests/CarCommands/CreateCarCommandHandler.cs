using CarRent.API.Domain.Entity;
using CarRent.API.Infraestructure.Persistence.Persistence;
using MediatR;

namespace CarRent.API.Application.Commands.Requests.CarCommands
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
                DailyPrice = request.DailyPrice,
                Year = request.Year
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            return car;
        }
    }
}
