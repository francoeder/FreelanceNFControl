using FluentValidation;
using FreelanceNFControl.Infra.Core.Requests.Expense;

namespace FreelanceNFControl.Infra.Core.Validators.Expense
{
    public class AddExpenseRequestValidator : AbstractValidator<AddExpenseRequest>
    {
        public AddExpenseRequestValidator()
        {
            RuleFor(request => request.Category).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.Value).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.Description).NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(request => request.PaymentDate)
                .Must(item => DateTime.TryParse(item, out var date)).WithMessage("{PropertyName} must be a valid date");
            RuleFor(request => request.CompetenceDate)
                .Must(item => DateTime.TryParse(item, out var date)).WithMessage("{PropertyName} must be a valid date");
        }
    }
}
