using AutoMapper;
using CarRent.API.Dtos;
using CarRent.Application.Commands.CarCommands;
using CarRent.Application.Queries.CarQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public CarController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _sender.Send(new GetCarsQuery());

            if (!cars.Any())
            {
                return NoContent();
            }

            return Ok(_mapper.Map<IEnumerable<CarDto>>(cars));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car = await _sender.Send(new GetCarByIdQuery(id));

            if (car is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CarDto>(car));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCar(CreateCarCommand command)
        {
            var car = await _sender.Send(command);

            if (car is null)
            {
                return BadRequest();
            }

            return Ok(car.Id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarById(UpdateCarCommand command)
        {
            var car = await _sender.Send(command);

            if (car is null)
            {
                return BadRequest();
            }

            return Ok(car.Id);
        }
    }
}
