namespace FreelanceNFControl.Infra.Core.Responses.Expense
{
    public class GetFinancialStatementResponse
    {
        public string UserId { get; set; }
        public int Year { get; set; }
        public List<MonthFinancialStatementResponse> MonthFinancialStatementList { get; set; } = new List<MonthFinancialStatementResponse>();
    }

    public class MonthFinancialStatementResponse
    {
        public string Month { get; set; }
        public int MonthNumber { get; set; }
        public decimal SummarizedInvoicesValue { get; set; }
        public decimal SummarizedExpensesValue { get; set; }
    }
}
