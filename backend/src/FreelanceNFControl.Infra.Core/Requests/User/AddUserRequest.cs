using FluentValidation.Results;
using FreelanceNFControl.Infra.Core.Validators.User;

namespace FreelanceNFControl.Infra.Core.Requests.User
{
    public class AddUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        private readonly AddUserRequestValidator _validator;

        public AddUserRequest()
        {
            _validator = new AddUserRequestValidator();
        }

        public bool IsValid() => _validator.Validate(this).IsValid;
        public List<ValidationFailure> GetErrors() => _validator.Validate(this).Errors;
    }
}
