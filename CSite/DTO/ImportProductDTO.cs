using CSite.Models;

namespace CSite.DTO
{
    public class ImportProductDTO
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public uint TotalPrice { get; set; }
        public uint Price { get; set; }
        public Product Product { get; set; }
    }
}

