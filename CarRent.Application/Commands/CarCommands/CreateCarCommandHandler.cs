﻿using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Commands.CarCommands
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Car>
    {
        private readonly ICarRepository _carRepository;

        public CreateCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Requisição recebida - Criar carro.");
            Car newCar = await _carRepository.CreateNewCar(request.Model, request.Maker, request.DailyPrice, request.Year);

            Console.WriteLine($"Carro {newCar.Id} criado.\n" +
                $"Requisição finalizada - Criar carro.");
            return newCar;
        }
    }
}
