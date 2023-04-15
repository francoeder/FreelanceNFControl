using FreelanceNFControl.Domain.Entities;

namespace FreelanceNFControl.Domain.Interfaces
{
    public interface IExpenseService
    {
        Task AddAsync(Expense entity);
    }
}
