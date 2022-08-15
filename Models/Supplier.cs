using System.ComponentModel.DataAnnotations;

namespace CSite.Models
{
    public class Supplier
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public uint Account { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<ImportReciept> ImportReciepts { get; set; } = new HashSet<ImportReciept>();

    }
}
