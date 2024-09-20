﻿using CarRent.Domain.Entities;
using CarRent.Domain.Events;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Services
{
    public class CarReturnedService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMediator _mediator;

        public CarReturnedService(ICarRepository carRepository, IMediator mediator)
        {
            _carRepository = carRepository;
            _mediator = mediator;
        }

        public async Task ProcessCarReturn(Rental rental)
        {
            Console.WriteLine($"{rental.Id} - Tornando carro disponível novamente.");
            await _carRepository.setCarAvailability(rental.RentedCar.Id, true);

            Console.WriteLine($"{rental.Id} - Carro {rental.RentedCar.Id} agora está disponível.");
            await _mediator.Publish(new PaymentEvent(rental));
        }
    }
}