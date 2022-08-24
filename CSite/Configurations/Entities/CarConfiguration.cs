using CSite.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CSite.Configurations.Entities
{
    public class CarConfiguration : IEntityTypeConfiguration<Cars>
    {
        public void Configure(EntityTypeBuilder<Cars> builder)
        {
            builder.HasData(
                new Cars
                {
                    Id = 20,
                    Name = "new car 1",
                    Account = 200,
                    Notes = "Empty"
                },
                new Cars
                {
                    Id = 21,
                    Name = "new car 2",
                    Account = 201,
                    Notes = "Empty"
                },
                new Cars
                {
                    Id = 22,
                    Name = "new car 3",
                    Account = 202,
                    Notes = "Empty"
                }
            );
        }
    }
}
