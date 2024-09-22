using AutoMapper;
using CarRent.Application.Dtos;
using CarRent.Application.Queries.CustomerQueries;
using CarRent.Application.Queries.PaymentReceiptQueries;
using CarRent.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentReceiptController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public PaymentReceiptController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentReceipt()
        {
            var paymentReceipts = await _sender.Send(new GetPaymentReceiptsQuery());
            return Ok(_mapper.Map<IEnumerable<PaymentReceiptDto>>(paymentReceipts));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentReceiptById(int id)
        {
            var paymentReceipt = await _sender.Send(new GetPaymentReceiptByIdQuery(id));

            if (paymentReceipt is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PaymentReceiptDto>(paymentReceipt));
        }
    }
}
