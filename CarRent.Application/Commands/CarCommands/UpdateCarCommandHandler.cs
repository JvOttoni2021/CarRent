using CarRent.Domain.Entities;
using CarRent.Domain.Exceptions;
using CarRent.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Commands.CarCommands
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Car?>
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<UpdateCarCommandHandler> _logger;

        public UpdateCarCommandHandler(ICarRepository carRepository, ILogger<UpdateCarCommandHandler> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<Car?> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Requisição recebida - Atualizar carro.");

                Car? car = _carRepository.GetCarById(request.Id);
                if (car == null) 
                    return null;
                
                car.Update(request.Maker, request.Model);
                await _carRepository.UpdateCar(car);

                _logger.LogInformation("Carro {Id} atualizado.", car.Id);
                return car;
            }
            catch (ArgumentNullException ex) {
                _logger.LogError(ex, "Dados informados na request são inválidos. Exception: {Message}", ex.Message);
            }

            return null;
        }
    }
}
