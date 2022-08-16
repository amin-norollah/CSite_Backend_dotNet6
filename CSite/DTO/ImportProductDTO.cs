namespace CSite.DTO
{
    public class ImportProductDTO
    {
        public int ImportReceiptID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
        public uint TotalPrice { get; set; }
        public uint BuyingPrice { get; set; }

    }
}

