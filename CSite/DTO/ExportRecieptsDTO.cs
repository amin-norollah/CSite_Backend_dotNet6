using System.ComponentModel.DataAnnotations;

namespace CSite.DTO
{
    public class ExportRecieptsDTO
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

        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
        public int? CarId { get; set; }
        public ICollection<ExportProductsDTO> ExportProducts { get; set; }

    }
}
