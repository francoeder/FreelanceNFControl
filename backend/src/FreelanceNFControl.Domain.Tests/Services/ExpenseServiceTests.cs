using AutoFixture;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Tests.Base;

namespace FreelanceNFControl.Domain.Tests.Services
{
    public class ExpenseServiceTests : IClassFixture<ExpenseServiceFixture>
    {
        private readonly ExpenseServiceFixture _fixture;

        public ExpenseServiceTests(ExpenseServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void Add_WhenReceiveAGoodExpense_ShouldPersistItCorrectly()
        {
            // Arrange
            var service = _fixture.GetInstance();
            var Expense = _fixture.Fixture.Build<Expense>()
                .Without(item => item.User)
                .Create();
            var userId = "45f333df-2a8d-419f-9000-8bb7391a8751";

            _fixture.HttpContextHelperMock
                .Setup(helper => helper.GetUserId())
                .Returns(userId);

            // Act
            await service.AddAsync(Expense);

            // Assert
            Assert.Single(_fixture.ExpenseListToVerify);
            
            var ExpenseToVerify = _fixture.ExpenseListToVerify.First();
            Assert.NotNull(ExpenseToVerify);
            Assert.Equal(userId, ExpenseToVerify.UserId);
            Assert.Equal(Expense.Category, ExpenseToVerify.Category);
            Assert.Equal(Expense.Value, ExpenseToVerify.Value);
            Assert.Equal(Expense.Description, ExpenseToVerify.Description);
            Assert.Equal(Expense.PaymentDate, ExpenseToVerify.PaymentDate);
            Assert.Equal(Expense.CompetenceDate, ExpenseToVerify.CompetenceDate);
            Assert.Equal(Expense.CustomerName, ExpenseToVerify.CustomerName);

        }
    }
}
