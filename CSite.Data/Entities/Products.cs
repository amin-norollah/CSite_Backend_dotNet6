using System;
using System.Collections.Generic;

namespace CSite.Data.Entities
{
    public partial class Products
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public long? Exported { get; set; }
        public long? Imported { get; set; }
        public int? CarsId { get; set; }

        public virtual Cars? Cars { get; set; }
    }
}
