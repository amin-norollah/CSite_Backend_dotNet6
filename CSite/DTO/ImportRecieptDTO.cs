namespace CSite.DTO
{
    public class ImportRecieptDTO
    {
        public int ID { get; set; }
        public uint Total { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public uint Paid { get; set; }
        public uint Remaining { get; set; }

        public int? SupplierID { get; set; }
        public int? UserID { get; set; }
        public ICollection<ImportProductDTO> importProducts { get; set; }
    }
}
