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
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasData(
                new Supplier
                {
                    ID = 400,
                    Name = "Supplier 1",
                    Phone = "+902020",
                    Account = 205,
                    Notes = "Empty"
                },
                new Supplier
                {
                    ID = 401,
                    Name = "Supplier 2",
                    Phone = "+902020",
                    Account = 206,
                    Notes = "Empty"
                },
                new Supplier
                {
                    ID = 402,
                    Name = "Supplier 3",
                    Phone = "+902020",
                    Account = 207,
                    Notes = "Empty"
                }
            );
        }
    }
}
