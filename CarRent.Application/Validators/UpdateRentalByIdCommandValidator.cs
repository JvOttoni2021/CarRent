using CarRent.Application.Commands.RentalCommands;
using CarRent.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.Validators
{
    public class UpdateRentalByIdCommandValidator : AbstractValidator<UpdateRentalByIdCommand>
    {
        public readonly IRentalRepository _rentalRepository;

        public UpdateRentalByIdCommandValidator(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;

            RuleFor(p => p.RentalId)
                .NotEmpty().WithMessage("Identificador da locação é obrigatório.")
                .Must(id =>
                {
                    return _rentalRepository.GetRentalById(id) is not null;
                }).WithMessage("Locação não existe.");


            RuleFor(d1 => d1.RentalDate)
                .LessThanOrEqualTo(d2 => d2.ExpectedReturnDate).WithMessage("Data prevista não pode ser menor que a de locação");
        }
    }
}
