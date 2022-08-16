using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSite.Models
{
    public class ExportReciept
    {
        [Key]
        public int ID { get; set; }
        public uint Total { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public uint Paid { get; set; }
        public uint Remaining { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Users User { get; set; }

        public virtual Car Car { get; set; }


        [ForeignKey("Customer")]
        public int? CustomerID { get; set; }
        [ForeignKey("User")]
        public string UserName { get; set; }
        [ForeignKey("Car")]
        public int? CarID { get; set; }
        public virtual ICollection<ExportProduct> ExportProducts { get; set; } = new HashSet<ExportProduct>();
    }
}
