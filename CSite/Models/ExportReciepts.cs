using System;
using System.Collections.Generic;

namespace CSite.Models
{
    public partial class ExportReciepts
    {
        public int Id { get; set; }
        public long Total { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public long Paid { get; set; }
        public long Remaining { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
        public int? CarId { get; set; }

        public virtual Cars Car { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<ExportProducts> ExportProducts { get; set; }
    }
}
