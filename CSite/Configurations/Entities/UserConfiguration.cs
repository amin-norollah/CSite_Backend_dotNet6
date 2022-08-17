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
                    UserName = "Amin",
                    Password = "csite@123",
                    Type = 0,
                    CarID = 20
                },
                new Users
                {
                    UserName = "Sara",
                    Password = "csite@123",
                    Type = 0,
                    CarID = 21
                },
                new Users
                {
                    UserName = "Javad",
                    Password = "csite@123",
                    Type = 1,
                    CarID = 22
                }
            );
        }
    }
}
