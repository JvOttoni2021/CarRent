using CarRent.API.Application.Commands.Requests.RentalCommands;
using CarRent.API.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Globalization;

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

            RuleFor(p => p.ExpectedReturnDate)
                .NotEmpty().WithMessage("Data prevista de retorno é obrigatória.")
                .Must(BeAValidDateTimeFormat).WithMessage("Data inválida, deve seguir o formato 'YYYY-MM-DDTmm:HH:ss'");
        }

        private bool BeAValidDateTimeFormat(DateTime time)
        {
            return !time.Equals(default(DateTime));
        }
    }
}
