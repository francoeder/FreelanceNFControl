using AutoFixture;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Tests.Base;

namespace FreelanceNFControl.Domain.Tests.Entities
{
    public class UserTests : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        public UserTests(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Constructor_ShouldCreateUser()
        {
            // Arrange
            var firstName = _fixture.Fixture.Create<string>();
            var lastName = _fixture.Fixture.Create<string>();
            var userName = _fixture.Fixture.Create<string>();
            var email = _fixture.Fixture.Create<string>();

            // Act
            var instance = new User(firstName, lastName, userName, email);

            // Assert
            Assert.Equal(firstName, instance.FirstName);
            Assert.Equal(lastName, instance.LastName);
            Assert.Equal(userName, instance.UserName);
            Assert.Equal(email, instance.Email);
        }
    }
}
