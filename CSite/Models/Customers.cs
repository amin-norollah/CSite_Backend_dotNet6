using System;
using System.Collections.Generic;

namespace CSite.Models
{
    public partial class Customers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public long Account { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<ExportReciepts> ExportReciepts { get; set; }
    }
}
