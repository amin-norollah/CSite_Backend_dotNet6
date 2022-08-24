using System;
using System.Collections.Generic;

namespace CSite.Models
{
    public partial class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long BuyingPrice { get; set; }
        public long SellingPrice { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<CarProducts> CarProducts { get; set; }
        public virtual ICollection<ExportProducts> ExportProducts { get; set; }
        public virtual ICollection<ImportProducts> ImportProducts { get; set; }
    }
}
