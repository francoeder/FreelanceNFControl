using FreelanceNFControl.Domain.DbContext;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Infra.Core.Helpers;

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
    }
}
