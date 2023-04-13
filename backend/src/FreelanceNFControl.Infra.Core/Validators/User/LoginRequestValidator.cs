using FluentValidation;
using FreelanceNFControl.Infra.Core.Requests.User;

namespace FreelanceNFControl.Infra.Core.Validators.User
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(request => request.UserNameOrEmail).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.Password).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
