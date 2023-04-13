using AutoFixture;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Services;
using FreelanceNFControl.Domain.Tests.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace FreelanceNFControl.Domain.Tests.Services.Fixtures
{
    public class UserServiceFixture : BaseFixture
    {
        public List<User> Users { get; protected set; } = new List<User>();
        public readonly Mock<UserManager<User>> UserManagerMock;
        public readonly Mock<SignInManager<User>> SignInManagerMock;
        
        public readonly IdentityResultMock IdentityResultFailed;
        public readonly IdentityResultMock IdentityResultSuccess;

        public readonly SignInResultMock SignInResultFailed;
        public readonly SignInResultMock SignInResultSuccess;

        public UserServiceFixture()
        {
            UserManagerMock = new Mock<UserManager<User>>(
                /* IUserStore<TUser> store */Mock.Of<IUserStore<User>>(),
                /* IOptions<IdentityOptions> optionsAccessor */null,
                /* IPasswordHasher<TUser> passwordHasher */null,
                /* IEnumerable<IUserValidator<TUser>> userValidators */null,
                /* IEnumerable<IPasswordValidator<TUser>> passwordValidators */null,
                /* ILookupNormalizer keyNormalizer */null,
                /* IdentityErrorDescriber errors */null,
                /* IServiceProvider services */null,
                /* ILogger<UserManager<TUser>> logger */null);

            SignInManagerMock = new Mock<SignInManager<User>>(
                UserManagerMock.Object,
                /* IHttpContextAccessor contextAccessor */Mock.Of<IHttpContextAccessor>(),
                /* IUserClaimsPrincipalFactory<TUser> claimsFactory */Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                /* IOptions<IdentityOptions> optionsAccessor */null,
                /* ILogger<SignInManager<TUser>> logger */null,
                /* IAuthenticationSchemeProvider schemes */null,
                /* IUserConfirmation<TUser> confirmation */null);

            IdentityResultFailed = new IdentityResultMock(false);
            IdentityResultSuccess = new IdentityResultMock(true);

            SignInResultFailed = new SignInResultMock(false);
            SignInResultSuccess = new SignInResultMock(true);
        }

        public UserService GetInstance()
        {
            return new UserService(UserManagerMock.Object, SignInManagerMock.Object);
        }
    }

    public class IdentityResultMock : IdentityResult
    {
        public IdentityResultMock(bool succeeded)
        {
            this.Succeeded = succeeded;
        }
    }

    public class SignInResultMock : SignInResult
    {
        public SignInResultMock(bool succeeded)
        {
            this.Succeeded = succeeded;
        }
    }
}
