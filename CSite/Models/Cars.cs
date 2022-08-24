using System;
using System.Collections.Generic;

namespace CSite.Models
{
    public partial class Cars
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Account { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<CarProducts> CarProducts { get; set; }
        public virtual ICollection<ExportReciepts> ExportReciepts { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
