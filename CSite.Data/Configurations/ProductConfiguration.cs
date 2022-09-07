using CSite.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSite.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasData(
                new Products
                {
                    Id = 300,
                    Quantity = 20,
                    Exported = 100,
                    Imported = 120,
                    CarsId = 20
                },
                new Products
                {
                    Id = 301,
                    Quantity = 50,
                    Exported = 230,
                    Imported = 280,
                    CarsId = 21
                },
                new Products
                {
                    Id = 302,
                    Quantity = 80,
                    Exported = 5,
                    Imported = 85,
                    CarsId = 22
                }
            );
        }
    }
}
