using CarRent.API.Application.Commands.RentalCommands;
using CarRent.API.Application.Queries.RentalQueries;
using CarRent.API.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly ISender _sender;

        public RentalController(ISender sender)
        {
            this._sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetRentals()
        {
            var rentals = await _sender.Send(new GetRentalsQuery());
            return Ok(rentals);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRental(CreateRentalCommand command)
        {
            var rentalToReturn = await _sender.Send(command);
            return Ok(rentalToReturn);
        }

        [HttpPut]
        public async Task<ActionResult> ReturnCar(ReturnCarCommand command)
        {
            Console.WriteLine("Requisição recebida - Devolução de automóvel");
            var rentalToReturn = await _sender.Send(command);
            Console.WriteLine("Requisição finalizada - Devolução de automóvel");
            return Ok(rentalToReturn.Id);
        }
    }
}
