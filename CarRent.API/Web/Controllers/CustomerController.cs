using CarRent.API.Application.Commands.CustomerCommands;
using CarRent.API.Application.Queries.CarQueries;
using CarRent.API.Application.Queries.CustomerQueries;
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
        private readonly ISender _sender;

        public CustomerController(ISender sender)
        {
            this._sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _sender.Send(new GetCustomersQuery());
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCustomer(CreateCustomerCommand command)
        {
            var customerToReturn = await _sender.Send(command);
            return Ok(customerToReturn.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _sender.Send(new GetCustomerByIdQuery(id));
            return Ok(customer);
        }
    }
}
