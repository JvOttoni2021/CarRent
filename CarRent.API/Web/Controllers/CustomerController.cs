using CarRent.API.Domain.Commands.Requests;
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

        [HttpPost]
        public async Task<ActionResult<int>> CreateCar(CreateCustomerCommand command)
        {
            var customerToReturn = await _sender.Send(command);
            return Ok(customerToReturn);
        }
    }
}
