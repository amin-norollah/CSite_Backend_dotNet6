using System.ComponentModel.DataAnnotations;

namespace CSite.Models
{
    public class Expenses
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
