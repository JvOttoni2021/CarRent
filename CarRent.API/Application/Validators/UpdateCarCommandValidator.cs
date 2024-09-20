using CarRent.API.Application.Commands.CarCommands;
using CarRent.API.Domain.Interfaces;
using FluentValidation;

namespace CarRent.API.Application.Validators
{
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {
        private readonly ICarRepository _carRepository;
        public UpdateCarCommandValidator(ICarRepository carRepository)
        {
            _carRepository = carRepository;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Identificador do carro é obrigatório")
                .Must(id =>
                {
                    return _carRepository.GetCarById(id) is not null;
                }).WithMessage("Carro não existe.");

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
