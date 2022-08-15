using CSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
