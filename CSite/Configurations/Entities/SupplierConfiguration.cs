using CSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSite.Configurations.Entities
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Suppliers>
    {
        public void Configure(EntityTypeBuilder<Suppliers> builder)
        {
            builder.HasData(
                new Suppliers
                {
                    Id = 400,
                    Name = "Supplier 1",
                    Phone = "+902020",
                    Account = 205,
                    Notes = "Empty"
                },
                new Suppliers
                {
                    Id = 401,
                    Name = "Supplier 2",
                    Phone = "+902020",
                    Account = 206,
                    Notes = "Empty"
                },
                new Suppliers
                {
                    Id = 402,
                    Name = "Supplier 3",
                    Phone = "+902020",
                    Account = 207,
                    Notes = "Empty"
                }
            );
        }
    }
}
