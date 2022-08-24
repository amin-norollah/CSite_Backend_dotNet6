using System.ComponentModel.DataAnnotations;

namespace CSite.DTO
{
    public class ImportRecieptsDTO
    {
        [Required]
        public uint Total { get; set; }
        public string Notes { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public uint Paid { get; set; }
        [Required]
        public uint Remaining { get; set; }

        public int? SupplierId { get; set; }
        public int? UserId { get; set; }
        public ICollection<ImportProductsDTO> importProducts { get; set; }
    }
}
