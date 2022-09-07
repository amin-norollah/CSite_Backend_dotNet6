using System.ComponentModel.DataAnnotations.Schema;

namespace CSite.Data.Entities
{
    [NotMapped]
    public partial class AspNetRoleClaims
    {
        public int Id { get; set; }
        public string RoleId { get; set; } = null!;
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        public virtual AspNetRoles Role { get; set; } = null!;
    }
}
