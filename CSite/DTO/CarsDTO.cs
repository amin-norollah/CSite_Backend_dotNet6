using System.ComponentModel.DataAnnotations;

namespace CSite.DTO
{
    public class CarsDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Name Is Too Long")]
        public string Name { get; set; }
        [Required]
        public uint Account { get; set; }
        public string Notes { get; set; }
    }
}
