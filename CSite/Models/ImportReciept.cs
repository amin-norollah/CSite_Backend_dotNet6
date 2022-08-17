using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSite.Models
{
    public class ImportReciept
    {
        [Key]
        public int ID { get; set; }
        public uint Total { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public uint Paid { get; set; }
        public uint Remaining { get; set; }


        public virtual Supplier Supplier { get; set; }
        public virtual Users User { get; set; }


        [ForeignKey("Supplier")]
        public int? SupplierID { get; set; }
        [ForeignKey("User")]
        public int? UserID { get; set; }

        public virtual ICollection<ImportProduct> ImportProducts { get; set; } = new HashSet<ImportProduct>();


    }
}
