using CarRent.Domain.Entities;
using CarRent.Domain.Events;
using CarRent.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.Commands.RentalCommands
{

    public class UpdateRentalByIdCommandHandler : IRequestHandler<UpdateRentalByIdCommand, int>
    {
        private readonly IRentalRepository _rentalRepository;

        public UpdateRentalByIdCommandHandler(
            IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<int> Handle(UpdateRentalByIdCommand request, CancellationToken cancellationToken)
        {

            int updatedRentalId = await _rentalRepository.UpdateRentalDatesById(request.RentalId, request.RentalDate, request.ExpectedReturnDate);

            return updatedRentalId;
        }
    }
}
