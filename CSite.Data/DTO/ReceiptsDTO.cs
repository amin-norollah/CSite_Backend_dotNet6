using System.ComponentModel.DataAnnotations;

namespace CSite.Data.DTO
{
    public class ReceiptsDTO
    {
        public int? Id { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public DateTime ReceiptDate { get; set; }
        public string? Notes { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public int ClientUserId { get; set; }
        [Required]
        public int SupplierUserId { get; set; }

    }
}
