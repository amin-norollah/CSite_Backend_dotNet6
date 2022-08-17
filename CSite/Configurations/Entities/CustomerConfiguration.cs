using CSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSite.Configurations.Entities
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new Supplier
                {
                    ID = 500,
                    Name = "Customer 1",
                    Phone = "+902020",
                    Account = 210,
                    Notes = "Empty"
                },
                new Supplier
                {
                    ID = 501,
                    Name = "Customer 2",
                    Phone = "+902020",
                    Account = 211,
                    Notes = "Empty"
                },
                new Supplier
                {
                    ID = 502,
                    Name = "Customer 3",
                    Phone = "+902020",
                    Account = 212,
                    Notes = "Empty"
                }
            );
        }
    }
}
