namespace CSite.DTO
{
    public class TransactionsDTO
    {
        public int ID { get; set; }
        public int? AccountID { get; set; }
        public int AccountType { get; set; }
        public uint Amount { get; set; }
        public int Type { get; set; }
        public int? OperationID { get; set; }
        public int Operation { get; set; }
        public string Date { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
    }
}
