using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Infra.Core.Requests.Invoice;
using FreelanceNFControl.Infra.Core.Responses.Invoice;

namespace FreelanceNFControl.Domain.Interfaces
{
    public interface IInvoiceService
    {
        Task AddAsync(Invoice entity);
        Task<SummarizedInvoicesValueResponse> GetSummarizedInvoicesValue(GetSummarizedInvoicesValueRequest request);
    }
}
