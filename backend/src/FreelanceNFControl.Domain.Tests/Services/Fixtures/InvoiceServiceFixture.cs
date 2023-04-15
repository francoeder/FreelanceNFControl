using FreelanceNFControl.Domain.DbContext;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Services;
using FreelanceNFControl.Infra.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FreelanceNFControl.Domain.Tests.Base
{
    public class InvoiceServiceFixture : BaseFixture
    {
        public readonly Mock<IHttpContextHelper> HttpContextHelperMock;
        public Mock<AppDbContext> AppDbContextMock;

        public Mock<DbSet<Invoice>> InvoicesDbSetMock;
        public List<Invoice> InvoiceListToVerify;

        public InvoiceServiceFixture()
        {
            HttpContextHelperMock = new Mock<IHttpContextHelper>();
            AppDbContextMock = new Mock<AppDbContext>();

            SetupDbContext();
        }

        public InvoiceService GetInstance()
        {
            return new InvoiceService(AppDbContextMock.Object, HttpContextHelperMock.Object);
        }

        private void SetupDbContext()
        {
            InvoiceListToVerify = new List<Invoice>();
            InvoicesDbSetMock = CreateDbSetMock(InvoiceListToVerify.AsQueryable());

            InvoicesDbSetMock
                .Setup(dbSet => dbSet.AddAsync(It.IsAny<Invoice>(), default))
                .Callback<Invoice, CancellationToken>((a, b) => InvoiceListToVerify.Add(a));

            AppDbContextMock
                .Setup(dbContext => dbContext.Invoices).Returns(InvoicesDbSetMock.Object);
        }

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> items) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>()
                .Setup(m => m.Expression).Returns(items.Expression);
            dbSetMock.As<IQueryable<T>>()
                .Setup(m => m.ElementType).Returns(items.ElementType);
            dbSetMock.As<IQueryable<T>>()
                .Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

            return dbSetMock;
        }
    }
}
