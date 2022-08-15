namespace CSite.DTO
{
    public class ImportRecieptDTO
    {
        public int ID { get; set; }
        public uint Total { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public uint Paid { get; set; }
        public uint Remaining { get; set; }

        public int? SUPID { get; set; }

        public string UserName { get; set; }
        public ImportProductDTO[] importProducts { get; set; }
    }
}
