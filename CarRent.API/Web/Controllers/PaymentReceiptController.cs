using CarRent.API.Application.Queries.PaymentReceiptQueries;
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
        private readonly ISender _sender;

        public PaymentReceiptController(ISender sender)
        {
            this._sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentReceipt()
        {
            var paymentReceipts = await _sender.Send(new GetPaymentReceiptsQuery());
            return Ok(paymentReceipts);
        }
    }
}
