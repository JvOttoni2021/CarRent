using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using CarRent.API.Infraestructure.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentReceiptController : ControllerBase
    {
        private readonly IPaymentReceiptRepository _paymentReceiptRepository;
        private readonly ISender _sender;

        public PaymentReceiptController(IPaymentReceiptRepository paymentReceiptRepository, ISender sender)
        {
            this._paymentReceiptRepository = paymentReceiptRepository;
            this._sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public IActionResult GetPaymentReceipt()
        {
            var customers = _paymentReceiptRepository.GetPaymentReceipts();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(customers);
        }
    }
}
