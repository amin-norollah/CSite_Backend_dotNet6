using System.ComponentModel.DataAnnotations.Schema;

namespace CSite.Data.Entities
{
    [NotMapped]
    public partial class AspNetUserLogins
    {
        public int Id { get; set; }
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }
        public string UserId { get; set; } = null!;

        public virtual AspNetUsers User { get; set; } = null!;
    }
}
