using CarRent.Application.Commands.CarCommands;
using FluentValidation;

namespace CarRent.Application.Validators
{
    public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(p => p.Model)
                .NotNull().WithMessage("Modelo não informado.")
                .Length(1, 100).WithMessage("Modelo deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.Maker)
                .NotNull().WithMessage("Marca não informada.")
                .Length(1, 100).WithMessage("Marca deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.DailyPrice)
                .GreaterThan(0).WithMessage("O preço diário deve ser um valor maior que 0.");

            RuleFor(p => p.Year)
                .NotNull().WithMessage("Ano não informado")
                .InclusiveBetween(1960, DateTime.Now.Year + 2).WithMessage("Ano inválido.");
        }
    }

}
