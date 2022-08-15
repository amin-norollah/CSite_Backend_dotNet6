using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSite.Models
{
    public class CarProduct
    {
        [Key]
        public int ID { get; set; }
        public int Quantity { get; set; }

        public virtual Car Car { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Car")]

        public int? CarID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
    }
}
