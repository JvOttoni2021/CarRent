using CarRent.API.Application.Commands.CustomerCommands;
using CarRent.API.Domain.Interfaces;
using FluentValidation;

namespace CarRent.API.Application.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(p => p.Name)
                .NotNull().WithMessage("Nome não informado.")
                .Length(1, 100).WithMessage("Nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.Cpf)
                .Must(id =>
                {
                    return !_customerRepository.CpfExists(id);
                }).WithMessage("Cpf já cadastrado.")
                .NotNull().WithMessage("Cpf não informado.")
                .Length(1, 11).WithMessage("Cpf deve ter entre 1 e 11 caracteres.");
        }
    }
}
