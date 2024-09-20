using CarRent.API.Application.Commands.CarCommands;
using CarRent.API.Application.Queries.CarQueries;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ISender _sender;

        public CarController(ISender sender)
        {
            this._sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _sender.Send(new GetCarsQuery());
            return Ok(cars);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCar([FromBody] CreateCarCommand command)
        {
            var car = await _sender.Send(command);
            return Ok(car.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car = await _sender.Send(new GetCarByIdQuery(id));
            return Ok(car);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCar(UpdateCarCommand command)
        {
            var car = await _sender.Send(command);
            return Ok(car);
        }
    }
}
