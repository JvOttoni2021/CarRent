using CarRent.API.Application.Repositories;
using CarRent.API.Domain.Commands.Requests.CustomerCommands;
using CarRent.API.Domain.Commands.Requests.RentalCommands;
using CarRent.API.Domain.Interfaces;
using FluentValidation;

namespace CarRent.API.Application.Validators
{
    public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
    {
        public readonly ICarRepository _carRepository;
        public readonly ICustomerRepository _customerRepository;

        public CreateRentalCommandValidator(ICarRepository carRepository, ICustomerRepository customerRepository)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;

            RuleFor(p => p.CarId)
                .NotNull().WithMessage("Identificador de carro é obrigatório")
                .Must(id =>
                {
                    return _carRepository.GetAvailableCarById(id) is not null;
                }).WithMessage("Carro não existe ou não está disponível");

            RuleFor(p => p.CustomerId)
                .NotNull().WithMessage("Identificador de carro é obrigatório")
                .Must(id =>
                {
                    return _customerRepository.GetCustomerById(id) is not null;
                }).WithMessage("Cliente não existe");
        }
    }
}
