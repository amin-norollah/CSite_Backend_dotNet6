using CSite.Models;

namespace CSite.DTO
{
    public class ExportProductDTO
    {
        public int ReceiptID { get; set; }

        public int Quantity { get; set; }
        public uint TotalPrice { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
