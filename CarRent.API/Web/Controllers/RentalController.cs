using CarRent.API.Application.Commands.Requests.RentalCommands;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ISender _sender;

        public RentalController(IRentalRepository rentalRepository, ISender sender)
        {
            this._rentalRepository = rentalRepository;
            this._sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Rental>))]
        public IActionResult GetRentals()
        {
            var rentals = _rentalRepository.GetRentals();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rentals);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateRental(CreateRentalCommand command)
        {
            var rentalToReturn = await _sender.Send(command);
            return Ok(rentalToReturn);
        }
    }
}
