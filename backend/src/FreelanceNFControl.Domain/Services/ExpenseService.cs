using FreelanceNFControl.Domain.DbContext;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Infra.Core.Helpers;
using FreelanceNFControl.Infra.Core.Requests.Expense;
using FreelanceNFControl.Infra.Core.Responses.Expense;
using Microsoft.EntityFrameworkCore;

namespace FreelanceNFControl.Domain.Services
{
    public class ExpenseService : IExpenseService
    {
        protected AppDbContext _dbContext;
        private readonly IHttpContextHelper _httpContextHelper;

        public ExpenseService(
            AppDbContext dbContext,
            IHttpContextHelper httpContextHelper)
        {
            _dbContext = dbContext;
            _httpContextHelper = httpContextHelper;
        }

        public async Task AddAsync(Expense entity)
        {
            var userId = _httpContextHelper.GetUserId();
            entity.UserId = userId;

            await _dbContext.Expenses.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<MonthFinancialStatementResponse>> GetFinancialStatement(GetFinancialStatementRequest request)
        {
            var userId = _httpContextHelper.GetUserId();

            var result = await _dbContext.Expenses
                .Where(expense => expense.PaymentDate.Year == request.Year && expense.UserId == userId)
                .GroupBy(expense => expense.PaymentDate.Month)
                .Select(s => new MonthFinancialStatementResponse { MonthNumber = s.Key, SummarizedExpensesValue = s.Sum(item => item.Value) })
                .ToListAsync();

            return result;
        }
    }
}
