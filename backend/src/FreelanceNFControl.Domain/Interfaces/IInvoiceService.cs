using FreelanceNFControl.Domain.Entities;

namespace FreelanceNFControl.Domain.Interfaces
{
    public interface IInvoiceService
    {
        Task AddAsync(Invoice entity);
    }
}
