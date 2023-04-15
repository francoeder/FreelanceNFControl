using AutoFixture;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Tests.Base;

namespace FreelanceNFControl.Domain.Tests.Services
{
    public class InvoiceServiceTests : IClassFixture<InvoiceServiceFixture>
    {
        private readonly InvoiceServiceFixture _fixture;

        public InvoiceServiceTests(InvoiceServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void Add_WhenReceiveAGoodInvoice_ShouldPersistItCorrectly()
        {
            // Arrange
            var service = _fixture.GetInstance();
            var invoice = _fixture.Fixture.Build<Invoice>()
                .Without(item => item.User)
                .Create();
            var userId = "45f333df-2a8d-419f-9000-8bb7391a8751";

            _fixture.HttpContextHelperMock
                .Setup(helper => helper.GetUserId())
                .Returns(userId);

            // Act
            await service.AddAsync(invoice);

            // Assert
            Assert.Single(_fixture.InvoiceListToVerify);
            
            var invoiceToVerify = _fixture.InvoiceListToVerify.First();
            Assert.NotNull(invoiceToVerify);
            Assert.Equal(userId, invoiceToVerify.UserId);
            Assert.Equal(invoice.CustomerName, invoiceToVerify.CustomerName);
            Assert.Equal(invoice.InvoiceNumber, invoiceToVerify.InvoiceNumber);
            Assert.Equal(invoice.Value, invoiceToVerify.Value);
            Assert.Equal(invoice.Description, invoiceToVerify.Description);
            Assert.Equal(invoice.Month, invoiceToVerify.Month);
            Assert.Equal(invoice.PaymentDate, invoiceToVerify.PaymentDate);
        }
    }
}
