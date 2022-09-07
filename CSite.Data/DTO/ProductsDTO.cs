using System.ComponentModel.DataAnnotations;

namespace CSite.Data.DTO
{
    public class ProductsDTO
    {
        public int? Id { get; set; }
        [Required]
        public uint Quantity { get; set; }
        public long? Exported { get; set; }
        public long? Imported { get; set; }
        [Required]
        public int CarsId { get; set; }
    }
}
