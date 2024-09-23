using CarRent.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.Commands.RentalCommands
{
    public record UpdateRentalByIdCommand(int RentalId, DateTime RentalDate, DateTime ExpectedReturnDate) : IRequest<int> { }
}
