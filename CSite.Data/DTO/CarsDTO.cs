using System.ComponentModel.DataAnnotations;

namespace CSite.Data.DTO
{
    public class CarsDTO
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Name Is Too Long")]
        public string Name { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float Price { get; set; }
        public string Image { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
