using CarRent.Application.Commands.RentalCommands;
using CarRent.Domain.Interfaces;
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
                .NotNull().WithMessage("Identificador de carro é obrigatório.")
                .Must(id =>
                {
                    return _carRepository.GetCarByIdAvailability(id, true) is not null;
                }).WithMessage("Carro não existe ou não está disponível.");

            RuleFor(p => p.CustomerId)
                .NotNull().WithMessage("Identificador de carro é obrigatório.")
                .Must(id =>
                {
                    return _customerRepository.GetCustomerById(id) is not null;
                }).WithMessage("Cliente não existe.");

            RuleFor(p => p.ExpectedReturnDate)
                .NotEmpty().WithMessage("Data prevista de retorno é obrigatória.")
                .Must(x => x > DateTime.Now).WithMessage("Data prevista de retorno deve ser uma data futura.");
        }
    }
}
