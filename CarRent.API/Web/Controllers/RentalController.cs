using AutoMapper;
using CarRent.Application.Commands.RentalCommands;
using CarRent.Application.Dtos;
using CarRent.Application.Queries.RentalQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ILogger<RentalController> _logger;

        public RentalController(ISender sender, IMapper mapper, ILogger<RentalController> logger)
        {
            _sender = sender;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetRentals()
        {
            var rentals = await _sender.Send(new GetRentalsQuery());
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

        [HttpPut]
        public async Task<ActionResult> ReturnCar(ReturnCarCommand command)
        {
            _logger.LogInformation("Requisição recebida - Devolução de automóvel");
            var rentalToReturn = await _sender.Send(command);

            if (rentalToReturn is null)
            {
                _logger.LogError("Erro na requisição - Devolução de automóvel");
                return BadRequest();
            }

            _logger.LogInformation("Requisição finalizada - Devolução de automóvel");
            return Ok(rentalToReturn.Id);
        }
    }
}
