namespace CSite.DTO
{
    public class TransactionsDTO
    {
        public int? AccountId { get; set; }
        public int AccountType { get; set; }
        public uint Amount { get; set; }
        public int Type { get; set; }
        public int? OperationId { get; set; }
        public int Operation { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Notes { get; set; }
    }
}
