using System;
using System.Collections.Generic;

namespace CSite.Models
{
    public partial class ExportProducts
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public long TotalPrice { get; set; }
        public long Price { get; set; }
        public int? ExportRecieptId { get; set; }
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }

        public virtual ExportReciepts ExportReciept { get; set; }
        public virtual Products Product { get; set; }
    }
}
