using FreelanceNFControl.Domain.DbContext;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Infra.Core.Helpers;

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
    }
}
