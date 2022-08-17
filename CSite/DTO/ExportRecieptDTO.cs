namespace CSite.DTO
{
    public class ExportRecieptDTO
    {
        public int ID { get; set; }
        public uint Total { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public uint Paid { get; set; }
        public uint Remaining { get; set; }
        public int? CustomerID { get; set; }
        public int? UserID { get; set; }
        public int? CarID { get; set; }
        public ICollection<ExportProductDTO> ExportProducts { get; set; }

    }
}
