using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Infra.Core.Requests.Expense;
using FreelanceNFControl.Infra.Core.Responses.Expense;

namespace FreelanceNFControl.Domain.Interfaces
{
    public interface IExpenseService
    {
        Task AddAsync(Expense entity);
        Task<List<MonthFinancialStatementResponse>> GetFinancialStatement(GetFinancialStatementRequest request);
    }
}
