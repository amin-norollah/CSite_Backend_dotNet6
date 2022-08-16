namespace CSite.DTO
{
    public class ExportRecieptDTO
    {
        public int ID { get; set; }
        public uint Total { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public uint Paid { get; set; }
        public uint Remaining { get; set; }
        public int? customerID { get; set; }
        public string UserName { get; set; }
        public int? CarID { get; set; }
        public ExportProductDTO[] Products { get; set; }

    }
}
