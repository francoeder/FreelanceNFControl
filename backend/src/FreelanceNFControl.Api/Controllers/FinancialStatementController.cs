using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Infra.Core.Requests.Expense;
using FreelanceNFControl.Infra.Core.Responses.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FreelanceNFControl.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FinancialStatementController : ControllerBase
    {
        public readonly IInvoiceService _invoiceService;
        public readonly IExpenseService _expenseService;

        public FinancialStatementController(
            IInvoiceService invoiceService,
            IExpenseService expenseService)
        {
            _invoiceService = invoiceService;
            _expenseService = expenseService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFinancialStatement([FromQuery] GetFinancialStatementRequest request)
        {
            var result = new GetFinancialStatementResponse();

            var invoiceFinancialStatements = await _invoiceService.GetFinancialStatement(request);
            var expenseFinancialStatements = await _expenseService.GetFinancialStatement(request);

            for (int i = 1; i <= 12; i++)
            {
                var monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                var invoiceStatement = invoiceFinancialStatements.FirstOrDefault(x => x.MonthNumber == i);
                var expenseStatement = expenseFinancialStatements.FirstOrDefault(x => x.MonthNumber == i);

                var statement = new MonthFinancialStatementResponse()
                {
                    Month = monthName,
                    MonthNumber = i,
                    SummarizedInvoicesValue = invoiceStatement != null ? invoiceStatement.SummarizedInvoicesValue : 0,
                    SummarizedExpensesValue = expenseStatement != null ? expenseStatement.SummarizedExpensesValue : 0,
                };

                result.MonthFinancialStatementList.Add(statement);
            }

            return Ok(result);
        }
    }
}
