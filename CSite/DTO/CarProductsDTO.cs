using CSite.Models;

namespace CSite.DTO
{
    public class CarProductsDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int? CarId { get; set; }
        public ProductsDTO Product { get; set; }
    }
}
