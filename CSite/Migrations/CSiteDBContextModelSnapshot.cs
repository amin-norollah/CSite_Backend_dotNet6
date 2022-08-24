﻿// <auto-generated />
using System;
using CSite.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CSite.Migrations
{
    [DbContext(typeof(CSiteDBContext))]
    partial class CSiteDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CSite.Models.CarProducts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CarId")
                        .HasColumnType("int")
                        .HasColumnName("CarId");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CarId" }, "IX_CarProducts_CarId");

                    b.HasIndex(new[] { "ProductId" }, "IX_CarProducts_ProductId");

                    b.ToTable("CarProducts");
                });

            modelBuilder.Entity("CSite.Models.Cars", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("Account")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 20,
                            Account = 200L,
                            Name = "new car 1",
                            Notes = "Empty"
                        },
                        new
                        {
                            Id = 21,
                            Account = 201L,
                            Name = "new car 2",
                            Notes = "Empty"
                        },
                        new
                        {
                            Id = 22,
                            Account = 202L,
                            Name = "new car 3",
                            Notes = "Empty"
                        });
                });

            modelBuilder.Entity("CSite.Models.Customers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("Account")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 500,
                            Account = 210L,
                            Name = "Customer 1",
                            Notes = "Empty",
                            Phone = "+902020"
                        },
                        new
                        {
                            Id = 501,
                            Account = 211L,
                            Name = "Customer 2",
                            Notes = "Empty",
                            Phone = "+902020"
                        },
                        new
                        {
                            Id = 502,
                            Account = 212L,
                            Name = "Customer 3",
                            Notes = "Empty",
                            Phone = "+902020"
                        });
                });

            modelBuilder.Entity("CSite.Models.Expenses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("CSite.Models.ExportProducts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ExportRecieptId")
                        .HasColumnType("int")
                        .HasColumnName("ExportRecieptId");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("int")
                        .HasColumnName("ReceiptId");

                    b.Property<long>("TotalPrice")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ExportRecieptId" }, "IX_ExportProducts_ExportRecieptId");

                    b.HasIndex(new[] { "ProductId" }, "IX_ExportProducts_ProductId");

                    b.ToTable("ExportProducts");
                });

            modelBuilder.Entity("CSite.Models.ExportReciepts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CarId")
                        .HasColumnType("int")
                        .HasColumnName("CarId");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Paid")
                        .HasColumnType("bigint");

                    b.Property<long>("Remaining")
                        .HasColumnType("bigint");

                    b.Property<long>("Total")
                        .HasColumnType("bigint");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CarId" }, "IX_ExportReciepts_CarId");

                    b.HasIndex(new[] { "CustomerId" }, "IX_ExportReciepts_CustomerId");

                    b.HasIndex(new[] { "UserId" }, "IX_ExportReciepts_UserId");

                    b.ToTable("ExportReciepts");
                });

            modelBuilder.Entity("CSite.Models.ImportProducts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("int")
                        .HasColumnName("ReceiptId");

                    b.Property<long>("TotalPrice")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ProductId" }, "IX_ImportProducts_ProductId");

                    b.HasIndex(new[] { "ReceiptId" }, "IX_ImportProducts_ReceiptId");

                    b.ToTable("ImportProducts");
                });

            modelBuilder.Entity("CSite.Models.ImportReciepts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Paid")
                        .HasColumnType("bigint");

                    b.Property<long>("Remaining")
                        .HasColumnType("bigint");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int")
                        .HasColumnName("SupplierId");

                    b.Property<long>("Total")
                        .HasColumnType("bigint");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "SupplierId" }, "IX_ImportReciepts_SupplierId");

                    b.HasIndex(new[] { "UserId" }, "IX_ImportReciepts_UserId");

                    b.ToTable("ImportReciepts");
                });

            modelBuilder.Entity("CSite.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("BuyingPrice")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long>("SellingPrice")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 300,
                            BuyingPrice = 10300L,
                            Name = "New Car 1",
                            Quantity = 10,
                            SellingPrice = 11660L
                        },
                        new
                        {
                            Id = 301,
                            BuyingPrice = 8400L,
                            Name = "New Car 2",
                            Quantity = 5,
                            SellingPrice = 9458L
                        },
                        new
                        {
                            Id = 302,
                            BuyingPrice = 15790L,
                            Name = "New Car 3",
                            Quantity = 8,
                            SellingPrice = 17820L
                        });
                });

            modelBuilder.Entity("CSite.Models.Suppliers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("Account")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");

                    b.HasData(
                        new
                        {
                            Id = 400,
                            Account = 205L,
                            Name = "Supplier 1",
                            Notes = "Empty",
                            Phone = "+902020"
                        },
                        new
                        {
                            Id = 401,
                            Account = 206L,
                            Name = "Supplier 2",
                            Notes = "Empty",
                            Phone = "+902020"
                        },
                        new
                        {
                            Id = 402,
                            Account = 207L,
                            Name = "Supplier 3",
                            Notes = "Empty",
                            Phone = "+902020"
                        });
                });

            modelBuilder.Entity("CSite.Models.Transactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("AccountId");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Operation")
                        .HasColumnType("int");

                    b.Property<int?>("OperationId")
                        .HasColumnType("int")
                        .HasColumnName("OperationId");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CSite.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CarId")
                        .HasColumnType("int")
                        .HasColumnName("CarId");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CarId" }, "IX_Users_CarId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CarId = 20,
                            Name = "Amin",
                            Password = "csite@123",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            CarId = 21,
                            Name = "Sara",
                            Password = "csite@123",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            CarId = 22,
                            Name = "Javad",
                            Password = "csite@123",
                            Type = 1
                        });
                });

            modelBuilder.Entity("CSite.Models.CarProducts", b =>
                {
                    b.HasOne("CSite.Models.Cars", "Car")
                        .WithMany("CarProducts")
                        .HasForeignKey("CarId");

                    b.HasOne("CSite.Models.Products", "Product")
                        .WithMany("CarProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CSite.Models.ExportProducts", b =>
                {
                    b.HasOne("CSite.Models.ExportReciepts", "ExportReciept")
                        .WithMany("ExportProducts")
                        .HasForeignKey("ExportRecieptId");

                    b.HasOne("CSite.Models.Products", "Product")
                        .WithMany("ExportProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExportReciept");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CSite.Models.ExportReciepts", b =>
                {
                    b.HasOne("CSite.Models.Cars", "Car")
                        .WithMany("ExportReciepts")
                        .HasForeignKey("CarId");

                    b.HasOne("CSite.Models.Customers", "Customer")
                        .WithMany("ExportReciepts")
                        .HasForeignKey("CustomerId");

                    b.HasOne("CSite.Models.Users", "User")
                        .WithMany("ExportReciepts")
                        .HasForeignKey("UserId");

                    b.Navigation("Car");

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CSite.Models.ImportProducts", b =>
                {
                    b.HasOne("CSite.Models.Products", "Product")
                        .WithMany("ImportProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSite.Models.ImportReciepts", "Receipt")
                        .WithMany("ImportProducts")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("CSite.Models.ImportReciepts", b =>
                {
                    b.HasOne("CSite.Models.Suppliers", "Supplier")
                        .WithMany("ImportReciepts")
                        .HasForeignKey("SupplierId");

                    b.HasOne("CSite.Models.Users", "User")
                        .WithMany("ImportReciepts")
                        .HasForeignKey("UserId");

                    b.Navigation("Supplier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CSite.Models.Users", b =>
                {
                    b.HasOne("CSite.Models.Cars", "Car")
                        .WithMany("Users")
                        .HasForeignKey("CarId");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CSite.Models.Cars", b =>
                {
                    b.Navigation("CarProducts");

                    b.Navigation("ExportReciepts");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CSite.Models.Customers", b =>
                {
                    b.Navigation("ExportReciepts");
                });

            modelBuilder.Entity("CSite.Models.ExportReciepts", b =>
                {
                    b.Navigation("ExportProducts");
                });

            modelBuilder.Entity("CSite.Models.ImportReciepts", b =>
                {
                    b.Navigation("ImportProducts");
                });

            modelBuilder.Entity("CSite.Models.Products", b =>
                {
                    b.Navigation("CarProducts");

                    b.Navigation("ExportProducts");

                    b.Navigation("ImportProducts");
                });

            modelBuilder.Entity("CSite.Models.Suppliers", b =>
                {
                    b.Navigation("ImportReciepts");
                });

            modelBuilder.Entity("CSite.Models.Users", b =>
                {
                    b.Navigation("ExportReciepts");

                    b.Navigation("ImportReciepts");
                });
#pragma warning restore 612, 618
        }
    }
}
