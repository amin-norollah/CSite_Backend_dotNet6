using CSite.Models;
using System.ComponentModel.DataAnnotations;

namespace CSite.DTO
{
    public class ExportProductsDTO
    {
        [Required]
        public int ReceiptId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public uint TotalPrice { get; set; }

        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
