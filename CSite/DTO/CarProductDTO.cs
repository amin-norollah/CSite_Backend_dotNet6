using CSite.Models;

namespace CSite.DTO
{
    public class CarProductDTO 
    {
        public int Quantity { get; set; }
        public int? CarID { get; set; }
        public ProductDTO Product { get; set; }


    }
}
