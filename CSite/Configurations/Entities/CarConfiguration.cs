using CSite.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CSite.Configurations.Entities
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(
                new Car
                {
                    ID = 20,
                    Name = "new car 1",
                    Account = 200,
                    Notes = "Empty"
                },
                new Car
                {
                    ID = 21,
                    Name = "new car 2",
                    Account = 201,
                    Notes = "Empty"
                },
                new Car
                {
                    ID = 22,
                    Name = "new car 3",
                    Account = 202,
                    Notes = "Empty"
                }
            );
        }
    }
}
