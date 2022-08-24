using System.ComponentModel.DataAnnotations;

namespace CSite.DTO
{
    public class ProductsDTO
    {
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Name Is Too Long")]
        public string Name { get; set; }
        public uint BuyingPrice { get; set; }
        public uint SellingPrice { get; set; }
        public int Quantity { get; set; }
    }
}
