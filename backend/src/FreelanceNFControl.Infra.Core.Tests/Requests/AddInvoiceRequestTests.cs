using AutoFixture;
using FreelanceNFControl.Infra.Core.Requests.Invoice;
using FreelanceNFControl.Infra.Core.Tests.Base;

namespace FreelanceNFControl.Infra.Core.Tests.Requests
{
    public class AddInvoiceRequestTests : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        public AddInvoiceRequestTests(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void IsValid_WhenPropertiesAreEmpty_ShouldReturnFalseWithCorrectMessages()
        {
            // Arrange
            var request = new AddInvoiceRequest() { Month = 1, PaymentDate = "2000-01-01" };

            // Act
            var isValid = request.IsValid();
            var errorMessages = request.GetErrors();

            // Assert
            Assert.False(isValid);

            Assert.Collection(errorMessages,
                error => Assert.Equal("Customer Name is required", error.ErrorMessage),
                error => Assert.Equal("Invoice Number is required", error.ErrorMessage),
                error => Assert.Equal("Value is required", error.ErrorMessage),
                error => Assert.Equal("Description is required", error.ErrorMessage));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(13)]
        public void IsValid_WhenMonthIsOutOfCorrectRange_ShouldReturnFalseWithCorrectMessages(int month)
        {
            // Arrange
            var request = _fixture.Fixture.Build<AddInvoiceRequest>()
                .With(request => request.Month, month)
                .With(request => request.PaymentDate, "2000-01-01")
                .Create();

            // Act
            var isValid = request.IsValid();
            var errorMessages = request.GetErrors();

            // Assert
            Assert.False(isValid);
            Assert.Single(errorMessages);
            Assert.Equal("Month needs to be a valid month (1 to 12)", errorMessages.First().ErrorMessage);
        }

        [Fact]
        public void IsValid_WhenDateFieldIsNotDate_ShouldReturnFalseWithCorrectMessages()
        {
            // Arrange
            var request = _fixture.Fixture.Build<AddInvoiceRequest>()
                .With(request => request.Month, 1)
                .With(request => request.PaymentDate, "-")
                .Create();

            // Act
            var isValid = request.IsValid();
            var errorMessages = request.GetErrors();

            // Assert
            Assert.False(isValid);
            Assert.Single(errorMessages);
            Assert.Equal("Payment Date must be a valid date", errorMessages.First().ErrorMessage);
        }
    }
}
