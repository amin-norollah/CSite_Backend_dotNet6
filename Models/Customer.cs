using System.ComponentModel.DataAnnotations;

namespace CSite.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public uint Account { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<ExportReciept> ExportReciepts { get; set; } = new HashSet<ExportReciept>();
    }
}
