using FluentValidation.Results;
using FreelanceNFControl.Infra.Core.Validators.Invoice;

namespace FreelanceNFControl.Infra.Core.Requests.Invoice
{
    public class AddInvoiceRequest
    {
        public string CustomerName { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public int Month { get; set; }
        public string PaymentDate { get; set; }

        private readonly AddInvoiceRequestValidator _validator;

        public AddInvoiceRequest()
        {
            _validator = new AddInvoiceRequestValidator();
        }

        public bool IsValid() => _validator.Validate(this).IsValid;
        public List<ValidationFailure> GetErrors() => _validator.Validate(this).Errors;
    }
}
