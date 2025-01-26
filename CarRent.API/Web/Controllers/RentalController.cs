using AutoMapper;
using CarRent.Application.Commands.RentalCommands;
using CarRent.Application.Dtos;
using CarRent.Application.Queries.RentalQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentalController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public RentalController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRentals()
        {
            var rentals = await _sender.Send(new GetRentalsQuery());

            if (!rentals.Any())
            {
                return NoContent();
            }

            return Ok(_mapper.Map<IEnumerable<RentalDto>>(rentals));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalById(int id)
        {
            var rental = await _sender.Send(new GetRentalByIdQuery(id));

            if (rental is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RentalDto>(rental));
        }

        [HttpPost]
        public async Task<ActionResult> CreateRental(CreateRentalCommand command)
        {
            var rentalToReturn = await _sender.Send(command);

            if (rentalToReturn is null)
            {
                return BadRequest();
            }

            return Ok(rentalToReturn.Id);
        }

        [HttpPut("return/{id}")]
        public async Task<ActionResult> ReturnCar(int id)
        {
            ReturnCarCommand command = new ReturnCarCommand(id);
            var rentalToReturn = await _sender.Send(command);

            if (rentalToReturn is null)
            {
                return NotFound("Rental não encontrada.");
            }

            return Ok(rentalToReturn.Id);
        }
    }
}
