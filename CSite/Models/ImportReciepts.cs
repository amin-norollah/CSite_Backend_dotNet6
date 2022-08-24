using System;
using System.Collections.Generic;

namespace CSite.Models
{
    public partial class ImportReciepts
    {
        public int Id { get; set; }
        public long Total { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public long Paid { get; set; }
        public long Remaining { get; set; }
        public int? SupplierId { get; set; }
        public int? UserId { get; set; }

        public virtual Suppliers Supplier { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<ImportProducts> ImportProducts { get; set; }
    }
}
