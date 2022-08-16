using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSite.Models
{
    public class ExportProduct
    {
        [Key]
        public int ID { get; set; }
        public int Quantity { get; set; }
        public uint TotalPrice { get; set; }
        public uint Price { get; set; }


        public virtual ExportReciept ExportReciept { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("EportReciept")]
        public int ReceiptID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
    }
}
