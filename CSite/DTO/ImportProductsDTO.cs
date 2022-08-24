using CSite.Models;
using System.ComponentModel.DataAnnotations;

namespace CSite.DTO
{
    public class ImportProductsDTO
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public uint TotalPrice { get; set; }
        [Required]
        public uint Price { get; set; }
        public Products Product { get; set; }
    }
}

