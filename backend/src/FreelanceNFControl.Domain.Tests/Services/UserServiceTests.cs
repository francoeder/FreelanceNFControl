using AutoFixture;
using FreelanceNFControl.Core.Responses.Login;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Tests.Services.Fixtures;
using FreelanceNFControl.Infra.Core.Requests.User;
using Moq;
using System.Security.Claims;

namespace FreelanceNFControl.Domain.Tests.Services
{
    public class UserServiceTests : IClassFixture<UserServiceFixture>
    {
        private readonly UserServiceFixture _fixture;

        public UserServiceTests(UserServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void Add_WhenUserManagerReturnSuccess_ShouldNotThrowException()
        {
            // Arrange
            var service = _fixture.GetInstance();
            var user = _fixture.Fixture.Build<User>()
                .Without(item => item.Invoices)
                .Without(item => item.Expenses)
                .Create();
            var password = _fixture.Fixture.Create<string>();

            _fixture.UserManagerMock
                .Setup(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(_fixture.IdentityResultSuccess);

            // Act
            var exception = await Record.ExceptionAsync(() => service.Add(user, password));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async void Add_WhenUserManagerReturnFailed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var service = _fixture.GetInstance();
            var user = _fixture.Fixture.Build<User>()
                .Without(item => item.Invoices)
                .Without(item => item.Expenses)
                .Create();
            var password = _fixture.Fixture.Create<string>();

            _fixture.UserManagerMock
                .Setup(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(_fixture.IdentityResultFailed);

            // Act
            var throwedException = await Record.ExceptionAsync(() => service.Add(user, password));

            // Assert
            Assert.NotNull(throwedException);
            Assert.IsType<InvalidOperationException>(throwedException);
            Assert.Equal("There was a problem creating the user.", throwedException.Message);
        }

        [Fact]
        public async void AuthenticateUser_WhenSignInManagerReturnSuccess_ShouldNotThrowException()
        {
            // Arrange
            var service = _fixture.GetInstance();
            var user = _fixture.Fixture.Build<User>()
                .With(user => user.Id, new Guid().ToString())
                .Without(item => item.Invoices)
                .Without(item => item.Expenses)
                .Create();
            var password = _fixture.Fixture.Create<string>();

            _fixture.SignInManagerMock
                .Setup(manager => manager.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(_fixture.SignInResultSuccess);

            _fixture.SignInManagerMock
                .Setup(manager => manager.CreateUserPrincipalAsync(It.IsAny<User>()))
                .ReturnsAsync(Mock.Of<ClaimsPrincipal>());

            // Act
            var response = await service.AuthenticateUser(user, password);

            // Assert
            Assert.IsType<LoginResponse>(response);
            Assert.Equal(user.Id, response.UserId.ToString());
            Assert.Equal(user.FirstName, response.Name);
            Assert.Equal(user.Email, response.Email);
            Assert.NotNull(response.AccessToken);
        }

        [Fact]
        public async void AuthenticateUser_WhenSignInManagerReturnFailed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var service = _fixture.GetInstance();
            var user = _fixture.Fixture.Build<User>()
                .Without(item => item.Invoices)
                .Without(item => item.Expenses)
                .Create();
            var password = _fixture.Fixture.Create<string>();

            _fixture.SignInManagerMock
                .Setup(manager => manager.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(_fixture.SignInResultFailed);

            // Act
            var throwedException = await Record.ExceptionAsync(() => service.AuthenticateUser(user, password));

            // Assert
            Assert.NotNull(throwedException);
            Assert.IsType<UnauthorizedAccessException>(throwedException);
            Assert.Equal("Invalid or unauthorized user.", throwedException.Message);
        }

        [Fact]
        public async void AuthenticateUserOverLoad_WhenSignInManagerReturnSuccess_ShouldNotThrowException()
        {
            // Arrange
            var service = _fixture.GetInstance();
            var loginRequest = _fixture.Fixture.Create<LoginRequest>();
            var user = _fixture.Fixture.Build<User>()
                .With(user => user.Id, new Guid().ToString())
                .Without(item => item.Invoices)
                .Without(item => item.Expenses)
                .Create();
            var password = _fixture.Fixture.Create<string>();

            _fixture.UserManagerMock
                .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _fixture.SignInManagerMock
                .Setup(manager => manager.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(_fixture.SignInResultSuccess);

            _fixture.SignInManagerMock
                .Setup(manager => manager.CreateUserPrincipalAsync(It.IsAny<User>()))
                .ReturnsAsync(Mock.Of<ClaimsPrincipal>());

            // Act
            var response = await service.AuthenticateUser(loginRequest);

            // Assert
            Assert.IsType<LoginResponse>(response);
            Assert.Equal(user.Id, response.UserId.ToString());
            Assert.Equal(user.FirstName, response.Name);
            Assert.Equal(user.Email, response.Email);
            Assert.NotNull(response.AccessToken);
        }

        [Fact]
        public async void AuthenticateUserOverLoad_WhenSignInManagerReturnFailed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var service = _fixture.GetInstance();
            var loginRequest = _fixture.Fixture.Create<LoginRequest>();
            var user = _fixture.Fixture.Build<User>()
                .Without(item => item.Invoices)
                .Without(item => item.Expenses)
                .Create();
            var password = _fixture.Fixture.Create<string>();

            _fixture.UserManagerMock
                .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _fixture.SignInManagerMock
                .Setup(manager => manager.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(_fixture.SignInResultFailed);

            // Act
            var throwedException = await Record.ExceptionAsync(() => service.AuthenticateUser(loginRequest));

            // Assert
            Assert.NotNull(throwedException);
            Assert.IsType<UnauthorizedAccessException>(throwedException);
            Assert.Equal("Invalid or unauthorized user.", throwedException.Message);
        }
    }
}
