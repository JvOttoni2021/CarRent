using CarRent.API.Domain.Commands.Requests;
using CarRent.API.Domain.Entity;
using FluentValidation;

namespace CarRent.API.Application.Validators
{
    public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(car => car.Model)
                .NotEmpty().WithMessage("Modelo não informado.")
                .Length(1, 100).WithMessage("Modelo deve ter entre 1 e 100 caracteres.");

            RuleFor(car => car.Maker)
                .NotEmpty().WithMessage("Marca não informada.")
                .Length(1, 100).WithMessage("Marca deve ter entre 1 e 100 caracteres.");

            RuleFor(car => car.Year)
                .NotEmpty().WithMessage("Ano não informado")
                .InclusiveBetween(1960, DateTime.Now.Year + 2).WithMessage("Ano inválido");
        }
    }
}
