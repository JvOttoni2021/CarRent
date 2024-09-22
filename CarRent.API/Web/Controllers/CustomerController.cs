using AutoMapper;
using CarRent.Application.Commands.CustomerCommands;
using CarRent.Application.Dtos;
using CarRent.Application.Queries.CustomerQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public CustomerController(ISender sender, IMapper mapper)
        {
            this._sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _sender.Send(new GetCustomersQuery());
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            var customer = await _sender.Send(command);

            if (customer is null)
            {
                return BadRequest();
            }

            return Ok(customer.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _sender.Send(new GetCustomerByIdQuery(id));

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerDto>(customer));
        }
    }
}
