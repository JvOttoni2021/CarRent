using CarRent.Application.Commands.RentalCommands;
using CarRent.Domain.Interfaces;
using FluentValidation;

namespace CarRent.API.Application.Validators
{
    public class ReturnCarCommandValidator : AbstractValidator<ReturnCarCommand>
    {
        public readonly IRentalRepository _rentalRepository;

        public ReturnCarCommandValidator(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Identificador da locação é obrigatório")
                .Must(id =>
                {
                    return _rentalRepository.GetUnfinishedRentalById(id) is not null;
                }).WithMessage("Locação não existe ou já foi concluída.");
        }
    }
}
