using FluentValidation.Results;
using FreelanceNFControl.Infra.Core.Validators.User;

namespace FreelanceNFControl.Infra.Core.Requests.User
{
    public class LoginRequest
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }

        private readonly LoginRequestValidator _validator;

        public LoginRequest()
        {
            _validator = new LoginRequestValidator();
        }

        public bool IsValid() => _validator.Validate(this).IsValid;
        public List<ValidationFailure> GetErrors() => _validator.Validate(this).Errors;
    }
}
