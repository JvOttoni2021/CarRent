using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Commands.CarCommands
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Car>
    {
        private readonly ICarRepository _carRepository;

        public UpdateCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Requisição recebida - Atualizar carro.");
            Car? newCar = await _carRepository.UpdateCar(request.Id, request.Model, request.Maker, request.DailyPrice, request.Year);

            Console.WriteLine($"Carro {newCar.Id} criado.\n" +
                $"Requisição finalizada - Criar carro.");
            return newCar;
        }
    }
}
