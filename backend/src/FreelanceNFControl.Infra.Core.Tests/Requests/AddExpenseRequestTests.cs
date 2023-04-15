using AutoFixture;
using FreelanceNFControl.Infra.Core.Requests.Expense;
using FreelanceNFControl.Infra.Core.Tests.Base;

namespace FreelanceNFControl.Infra.Core.Tests.Requests
{
    public class AddExpenseRequestTests : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        public AddExpenseRequestTests(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void IsValid_WhenPropertiesAreEmpty_ShouldReturnFalseWithCorrectMessages()
        {
            // Arrange
            var request = new AddExpenseRequest() { PaymentDate = "2000-01-01", CompetenceDate = "2000-01-01" };

            // Act
            var isValid = request.IsValid();
            var errorMessages = request.GetErrors();

            // Assert
            Assert.False(isValid);

            Assert.Collection(errorMessages,
                error => Assert.Equal("Category is required", error.ErrorMessage),
                error => Assert.Equal("Value is required", error.ErrorMessage),
                error => Assert.Equal("Description is required", error.ErrorMessage));
        }

        [Fact]
        public void IsValid_WhenDateFieldsAreNotDates_ShouldReturnFalseWithCorrectMessages()
        {
            // Arrange
            var request = _fixture.Fixture.Build<AddExpenseRequest>()
                .With(request => request.PaymentDate, "-")
                .With(request => request.CompetenceDate, "-")
                .Create();

            // Act
            var isValid = request.IsValid();
            var errorMessages = request.GetErrors();

            // Assert
            Assert.False(isValid);
            Assert.Collection(errorMessages,
                error => Assert.Equal("Payment Date must be a valid date", error.ErrorMessage),
                error => Assert.Equal("Competence Date must be a valid date", error.ErrorMessage));
        }
    }
}
