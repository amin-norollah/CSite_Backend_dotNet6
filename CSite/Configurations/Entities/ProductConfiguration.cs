using CSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSite.Configurations.Entities
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    ID = 300,
                    Name = "New Car 1",
                    BuyingPrice = 10300,
                    SellingPrice = 11660,
                    Quantity = 10
                },
                new Product
                {
                    ID = 301,
                    Name = "New Car 2",
                    BuyingPrice = 8400,
                    SellingPrice = 9458,
                    Quantity = 5
                },
                new Product
                {
                    ID = 302,
                    Name = "New Car 3",
                    BuyingPrice = 15790,
                    SellingPrice = 17820,
                    Quantity = 8
                }
            );
        }
    }
}
