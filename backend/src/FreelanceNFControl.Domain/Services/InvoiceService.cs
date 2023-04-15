using FreelanceNFControl.Domain.DbContext;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Infra.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace FreelanceNFControl.Domain.Services
{
    public class InvoiceService : IInvoiceService
    {
        protected AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InvoiceService(
            AppDbContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddAsync(Invoice entity)
        {
            var userId = GetUserId();
            entity.UserId = userId;

            await _dbContext.Invoices.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        private string GetUserId()
        {
            var decodedToken = JwtHelper.DecryptToken(GetTokenFromRequest());

            if (!decodedToken.Claims.Any(c => c.Type == "nameid"))
                throw new UnauthorizedAccessException("Invalid Token");
            
            return decodedToken.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
        }

        private string GetTokenFromRequest()
        {
            var headerToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();
            var bearerToken = headerToken.Replace("Bearer", string.Empty).Trim();
            return bearerToken;
        }
    }
}
