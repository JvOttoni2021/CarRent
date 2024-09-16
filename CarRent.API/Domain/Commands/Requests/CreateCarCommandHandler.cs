using CarRent.API.Application.Persistence;
using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Domain.Commands.Requests
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, int>
    {
        private readonly CarRentContext context;

        public CreateCarCommandHandler(CarRentContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = new Car
            {
                Model = request.Model,
                CarMaker = request.CarMaker,
                Year = request.Year
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            return car.Id;
        }
    }
}
