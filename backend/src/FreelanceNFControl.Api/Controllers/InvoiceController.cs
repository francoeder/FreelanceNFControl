using AutoMapper;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Infra.Core.Requests.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceNFControl.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;

        public InvoiceController(
            IInvoiceService invoiceService,
            IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddInvoice(AddInvoiceRequest request)
        {
            if (!request.IsValid())
            {
                return BadRequest(request.GetErrors());
            }

            var invoice = _mapper.Map<Invoice>(request);

            await _invoiceService.AddAsync(invoice);

            return Ok(invoice);
        }

        [HttpGet("summarized")]
        [Authorize]
        public async Task<IActionResult> GetYear([FromQuery] GetSummarizedInvoicesValueRequest request)
        {
            var result = await _invoiceService.GetSummarizedInvoicesValue(request);
            return Ok(result);
        }
    }
}
