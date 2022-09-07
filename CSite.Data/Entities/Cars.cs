using System;
using System.Collections.Generic;

namespace CSite.Data.Entities
{
    public partial class Cars
    {
        public Cars()
        {
            Products = new HashSet<Products>();
            Receipts = new HashSet<Receipts>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Description { get; set; } = null!;
        public string? Image { get; set; }
        public string UserId { get; set; } = null!;

        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<Receipts> Receipts { get; set; }
    }
}
