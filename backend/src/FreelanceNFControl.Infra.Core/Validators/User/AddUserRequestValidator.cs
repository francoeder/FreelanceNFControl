using FluentValidation;
using FreelanceNFControl.Infra.Core.Requests.User;

namespace FreelanceNFControl.Infra.Core.Validators.User
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(request => request.FirstName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.UserName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.Email).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Equal(request => request.PasswordConfirmation).WithMessage("Password and PasswordConfirmation do not match");
        }
    }
}
