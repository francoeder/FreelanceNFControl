namespace FreelanceNFControl.Infra.Core.Responses.Invoice
{
    public class SummarizedInvoicesValueResponse
    {
        public string UserId { get; set; }
        public int Year { get; set; }
        public decimal SummarizedInvoicesValue { get; set; }
        public decimal AnnualBillingThreshold { get; set; }
        public decimal PercentageAlreadyReached { get { return Math.Round(Math.Min(SummarizedInvoicesValue / AnnualBillingThreshold, 100), 2); } }
    }
}
