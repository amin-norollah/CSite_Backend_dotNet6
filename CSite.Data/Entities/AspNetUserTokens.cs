using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSite.Data.Entities
{
    [NotMapped]
    public partial class AspNetUserTokens
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }

        public virtual AspNetUsers User { get; set; } = null!;
    }
}
