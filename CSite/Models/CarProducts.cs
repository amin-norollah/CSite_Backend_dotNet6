using System;
using System.Collections.Generic;

namespace CSite.Models
{
    public partial class CarProducts
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int? CarId { get; set; }
        public int ProductId { get; set; }

        public virtual Cars Car { get; set; }
        public virtual Products Product { get; set; }
    }
}
