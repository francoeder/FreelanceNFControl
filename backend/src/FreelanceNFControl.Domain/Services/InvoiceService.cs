﻿using FreelanceNFControl.Domain.DbContext;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Domain.Migrations;
using FreelanceNFControl.Infra.Core.Helpers;
using FreelanceNFControl.Infra.Core.Requests.Expense;
using FreelanceNFControl.Infra.Core.Requests.Invoice;
using FreelanceNFControl.Infra.Core.Responses.Expense;
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

        public async Task AddAsync(Entities.Invoice entity)
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

        public async Task<List<MonthFinancialStatementResponse>> GetFinancialStatement(GetFinancialStatementRequest request)
        {
            var userId = _httpContextHelper.GetUserId();

            var result = await _dbContext.Invoices
                .Where(invoice => invoice.PaymentDate.Year == request.Year && invoice.UserId == userId)
                .GroupBy(invoice => invoice.PaymentDate.Month)
                .Select(s => new MonthFinancialStatementResponse { MonthNumber = s.Key, SummarizedInvoicesValue = s.Sum(item => item.Value) })
                .ToListAsync();

            return result;
        }
    }
}
