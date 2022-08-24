using CSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSite.Configurations.Entities
{
    public class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasData(
                new Products
                {
                    Id = 300,
                    Name = "New Car 1",
                    BuyingPrice = 10300,
                    SellingPrice = 11660,
                    Quantity = 10
                },
                new Products
                {
                    Id = 301,
                    Name = "New Car 2",
                    BuyingPrice = 8400,
                    SellingPrice = 9458,
                    Quantity = 5
                },
                new Products
                {
                    Id = 302,
                    Name = "New Car 3",
                    BuyingPrice = 15790,
                    SellingPrice = 17820,
                    Quantity = 8
                }
            );
        }
    }
}
