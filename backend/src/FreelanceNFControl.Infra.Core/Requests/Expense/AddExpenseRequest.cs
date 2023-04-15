using FluentValidation.Results;
using FreelanceNFControl.Infra.Core.Validators.Expense;

namespace FreelanceNFControl.Infra.Core.Requests.Expense
{
    public class AddExpenseRequest
    {
        public string Category { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string PaymentDate { get; set; }
        public string CompetenceDate { get; set; }
        public string? CustomerName { get; set; }

        private readonly AddExpenseRequestValidator _validator;

        public AddExpenseRequest()
        {
            _validator = new AddExpenseRequestValidator();
        }

        public bool IsValid() => _validator.Validate(this).IsValid;
        public List<ValidationFailure> GetErrors() => _validator.Validate(this).Errors;
    }
}
