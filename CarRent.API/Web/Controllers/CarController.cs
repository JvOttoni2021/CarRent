using CarRent.API.Domain.Commands.Requests;
using CarRent.API.Domain.Commands.Responses;
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
        private readonly ICarRepository _carRepository;
        private readonly ISender _sender;

        public CarController(ICarRepository carRepository, ISender sender)
        {
            this._carRepository = carRepository;
            this._sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCars()
        {
            var cars = _carRepository.GetCars();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(cars);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCar(CreateCarCommand command)
        {
            var carToReturn = await _sender.Send(command);
            return Ok(carToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car = await _sender.Send(new GetCarByIdQuery(id));
            return Ok(car);
        }
    }
}
