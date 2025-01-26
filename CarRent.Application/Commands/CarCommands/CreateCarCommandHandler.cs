using CarRent.Domain.Entities;
using CarRent.Domain.Exceptions;
using CarRent.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Commands.CarCommands
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Car?>
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<CreateCarCommandHandler> _logger;

        public CreateCarCommandHandler(ICarRepository carRepository, ILogger<CreateCarCommandHandler> carLogger)
        {
            _carRepository = carRepository;
            _logger = carLogger;
        }

        public async Task<Car?> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Requisição recebida - Criar carro.");
                Car newCar = new Car(request.Model, request.Maker, request.Year, request.DailyPrice);

                await _carRepository.CreateCar(newCar);

                _logger.LogInformation("Carro {NewCarId} criado.", newCar.Id);
                return newCar;
            }
            catch (DomainException ex) {
                _logger.LogError(ex, "Não foi possível criar o carro, parâmetros inválidos.");
            }

            return null;
        }
    }
}
