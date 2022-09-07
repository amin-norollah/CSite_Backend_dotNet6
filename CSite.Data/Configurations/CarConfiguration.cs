
using CSite.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CSite.Data.Configurations
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
                    Price = 100,
                    Image = "",
                    CreatedDate = new DateTime(2021, 10, 10),
                    ModifiedDate = new DateTime(2022, 9, 10),
                    Description = "Empty",
                    UserId = "4cd6cba2-4950-42a2-bb48-14f09aef239d"
                },
                new Cars
                {
                    Id = 21,
                    Name = "new car 1",
                    Price = 100,
                    Image = "",
                    CreatedDate = new DateTime(2020, 5, 5),
                    ModifiedDate = new DateTime(2022, 1, 9),
                    Description = "Empty",
                    UserId = "6cd6cba2-4950-42a2-bb48-14f09aef239d"
                },
                new Cars
                {
                    Id = 22,
                    Name = "new car 1",
                    Price = 100,
                    Image = "",
                    CreatedDate = new DateTime(2022, 3, 12),
                    ModifiedDate = new DateTime(2022, 6, 22),
                    Description = "Empty",
                    UserId = "6cd6cba2-4950-42a2-bb48-14f09aef239d"
                }
            );
        }
    }
}
