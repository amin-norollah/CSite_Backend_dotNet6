using System.ComponentModel.DataAnnotations.Schema;

namespace CSite.Data.Entities
{
    [NotMapped]
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaims>();
            User = new HashSet<AspNetUsers>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; }

        public virtual ICollection<AspNetUsers> User { get; set; }
    }
}
