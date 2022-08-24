using System;
using System.Collections.Generic;

namespace CSite.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
        public int? CarId { get; set; }

        public virtual Cars Car { get; set; }
        public virtual ICollection<ExportReciepts> ExportReciepts { get; set; }
        public virtual ICollection<ImportReciepts> ImportReciepts { get; set; }
    }
}
