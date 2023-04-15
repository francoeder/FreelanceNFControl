using FreelanceNFControl.Domain.DbContext;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Services;
using FreelanceNFControl.Infra.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FreelanceNFControl.Domain.Tests.Base
{
    public class ExpenseServiceFixture : BaseFixture
    {
        public readonly Mock<IHttpContextHelper> HttpContextHelperMock;
        public Mock<AppDbContext> AppDbContextMock;

        public Mock<DbSet<Expense>> ExpensesDbSetMock;
        public List<Expense> ExpenseListToVerify;

        public ExpenseServiceFixture()
        {
            HttpContextHelperMock = new Mock<IHttpContextHelper>();
            AppDbContextMock = new Mock<AppDbContext>();

            SetupDbContext();
        }

        public ExpenseService GetInstance()
        {
            return new ExpenseService(AppDbContextMock.Object, HttpContextHelperMock.Object);
        }

        private void SetupDbContext()
        {
            ExpenseListToVerify = new List<Expense>();
            ExpensesDbSetMock = CreateDbSetMock(ExpenseListToVerify.AsQueryable());

            ExpensesDbSetMock
                .Setup(dbSet => dbSet.AddAsync(It.IsAny<Expense>(), default))
                .Callback<Expense, CancellationToken>((a, b) => ExpenseListToVerify.Add(a));

            AppDbContextMock
                .Setup(dbContext => dbContext.Expenses).Returns(ExpensesDbSetMock.Object);
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
