using AutoFixture;
using FreelanceNFControl.Infra.Core.Requests.User;
using FreelanceNFControl.Infra.Core.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceNFControl.Infra.Core.Tests.Requests
{
    public class AddUserRequestTests : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        public AddUserRequestTests(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void IsValid_WhenPropertiesAreEmpty_ShouldReturnFalseWithCorrectMessages()
        {
            // Arrange
            var request = new AddUserRequest();

            // Act
            var isValid = request.IsValid();
            var errorMessages = request.GetErrors();

            // Assert
            Assert.False(isValid);

            Assert.Collection(errorMessages, 
                error => Assert.Equal("First Name is required", error.ErrorMessage),
                error => Assert.Equal("User Name is required", error.ErrorMessage),
                error => Assert.Equal("Email is required", error.ErrorMessage),
                error => Assert.Equal("Password is required", error.ErrorMessage));
        }

        [Fact]
        public void IsValid_WhenPasswordsNotMatch_ShouldReturnFalseWithCorrectMessages()
        {
            // Arrange
            var request = _fixture.Fixture.Build<AddUserRequest>()
                .With(request => request.Password, "a")
                .With(request => request.PasswordConfirmation, "b")
                .Create();

            // Act
            var isValid = request.IsValid();
            var errorMessages = request.GetErrors();

            // Assert
            Assert.False(isValid);
            Assert.Single(errorMessages);
            Assert.Equal("Password and PasswordConfirmation do not match", errorMessages.First().ErrorMessage);
        }
    }
}
