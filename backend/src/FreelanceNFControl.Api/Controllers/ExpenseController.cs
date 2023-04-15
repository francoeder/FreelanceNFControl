using AutoMapper;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Infra.Core.Requests.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceNFControl.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _ExpenseService;
        private readonly IMapper _mapper;

        public ExpenseController(
            IExpenseService ExpenseService,
            IMapper mapper)
        {
            _ExpenseService = ExpenseService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddExpense(AddExpenseRequest request)
        {
            if (!request.IsValid())
            {
                return BadRequest(request.GetErrors());
            }

            var Expense = _mapper.Map<Expense>(request);

            await _ExpenseService.AddAsync(Expense);

            return Ok(Expense);
        }
    }
}
