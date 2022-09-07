using System;
using System.Collections.Generic;

namespace CSite.Data.Entities
{
    public partial class Receipts
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string? Notes { get; set; }
        public string ClientUserId { get; set; } = null!;
        public string SupplierUserId { get; set; } = null!;
        public int CarId { get; set; }

        public virtual Cars Car { get; set; } = null!;
    }
}
