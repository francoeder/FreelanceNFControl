using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Infra.Core.Requests.Expense;
using FreelanceNFControl.Infra.Core.Requests.Invoice;
using FreelanceNFControl.Infra.Core.Responses.Expense;
using FreelanceNFControl.Infra.Core.Responses.Invoice;

namespace FreelanceNFControl.Domain.Interfaces
{
    public interface IInvoiceService
    {
        Task AddAsync(Invoice entity);
        Task<SummarizedInvoicesValueResponse> GetSummarizedInvoicesValue(GetSummarizedInvoicesValueRequest request);
        Task<List<MonthFinancialStatementResponse>> GetFinancialStatement(GetFinancialStatementRequest request);
    }
}
