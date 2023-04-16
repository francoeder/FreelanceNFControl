using FreelanceNFControl.Domain.DbContext;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Infra.Core.Helpers;
using FreelanceNFControl.Infra.Core.Requests.Invoice;
using FreelanceNFControl.Infra.Core.Responses.Invoice;
using Microsoft.EntityFrameworkCore;

namespace FreelanceNFControl.Domain.Services
{
    public class InvoiceService : IInvoiceService
    {
        protected AppDbContext _dbContext;
        private readonly IHttpContextHelper _httpContextHelper;

        public InvoiceService(
            AppDbContext dbContext,
            IHttpContextHelper httpContextHelper)
        {
            _dbContext = dbContext;
            _httpContextHelper = httpContextHelper;
        }

        public async Task AddAsync(Invoice entity)
        {
            var userId = _httpContextHelper.GetUserId();
            entity.UserId = userId;

            await _dbContext.Invoices.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<SummarizedInvoicesValueResponse> GetSummarizedInvoicesValue(GetSummarizedInvoicesValueRequest request)
        {
            var userId = _httpContextHelper.GetUserId();
            var summarizedValue = await _dbContext.Invoices
                .Where(invoice => invoice.UserId == userId && invoice.PaymentDate.Year == request.Year)
                .SumAsync(invoice => invoice.Value);

            return new SummarizedInvoicesValueResponse()
            {
                UserId = userId,
                Year = request.Year,
                SummarizedInvoicesValue = summarizedValue,
                AnnualBillingThreshold = 81000m
            };
        }
    }
}
