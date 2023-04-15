namespace FreelanceNFControl.Domain.Entities
{
    public class Expense
    {
        public Expense()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Category { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime CompetenceDate { get; set; }
        public string? CustomerName { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
