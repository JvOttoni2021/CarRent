using CarRent.API.Application.Commands.Requests.CustomerCommands;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISender _sender;

        public CustomerController(ICustomerRepository customerRepository, ISender sender)
        {
            this._customerRepository = customerRepository;
            this._sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public IActionResult GetCustomers()
        {
            var customers = _customerRepository.GetCustomers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCar(CreateCustomerCommand command)
        {
            var customerToReturn = await _sender.Send(command);
            return Ok(customerToReturn);
        }
    }
}
