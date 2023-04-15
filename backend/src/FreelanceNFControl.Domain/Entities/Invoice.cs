namespace FreelanceNFControl.Domain.Entities
{
    public class Invoice
    {
        protected Invoice()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string CustomerName { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public int Month { get; set; }
        public DateTime PaymentDate { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
