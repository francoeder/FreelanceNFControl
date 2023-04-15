using FluentValidation;
using FreelanceNFControl.Infra.Core.Requests.Invoice;

namespace FreelanceNFControl.Infra.Core.Validators.Invoice
{
    public class AddInvoiceRequestValidator : AbstractValidator<AddInvoiceRequest>
    {
        public AddInvoiceRequestValidator()
        {
            RuleFor(request => request.CustomerName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.InvoiceNumber).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.Value).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.Description).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(request => request.Month).InclusiveBetween(1, 12).WithMessage("{PropertyName} needs to be a valid month (1 to 12)");
            RuleFor(request => request.PaymentDate)
                .Must(item => DateTime.TryParse(item, out var date)).WithMessage("{PropertyName} must be a valid date");
        }
    }
}
