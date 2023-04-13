using AutoFixture;
using FreelanceNFControl.Infra.Core.Requests.User;
using FreelanceNFControl.Infra.Core.Tests.Base;

namespace FreelanceNFControl.Infra.Core.Tests.Requests
{
    public class LoginRequestTests : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        public LoginRequestTests(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void IsValid_WhenPropertiesAreEmpty_ShouldReturnFalseWithCorrectMessages()
        {
            // Arrange
            var request = new LoginRequest();

            // Act
            var isValid = request.IsValid();
            var errorMessages = request.GetErrors();

            // Assert
            Assert.False(isValid);

            Assert.Collection(errorMessages, 
                error => Assert.Equal("User Name Or Email is required", error.ErrorMessage),
                error => Assert.Equal("Password is required", error.ErrorMessage));
        }
    }
}
