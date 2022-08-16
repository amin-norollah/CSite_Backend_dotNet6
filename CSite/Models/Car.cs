using System.ComponentModel.DataAnnotations;

namespace CSite.Models
{
    public class Car
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public uint Account { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<CarProduct> CarProducts { get; set; } = new HashSet<CarProduct>();//API GET products of car  (id car )

        public virtual ICollection<ExportReciept> ExportReciepts { get; set; } = new HashSet<ExportReciept>();

        public virtual ICollection<Users> Users { get; set; } = new HashSet<Users>();
    }
}
