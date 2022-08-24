using CSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSite.Configurations.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasData(
                new Users
                {
                    Id = 1,
                    Name = "Amin",
                    Password = "csite@123",
                    Type = 0,
                    CarId = 20
                },
                new Users
                {
                    Id=2,
                    Name = "Sara",
                    Password = "csite@123",
                    Type = 0,
                    CarId = 21
                },
                new Users
                {
                    Id =3,
                    Name = "Javad",
                    Password = "csite@123",
                    Type = 1,
                    CarId = 22
                }
            );
        }
    }
}
