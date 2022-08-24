using System;
using System.Collections.Generic;
using CSite.Configurations.Entities;
using CSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CSite.DbContexts
{
    public partial class CSiteDBContext : DbContext
    {
        public CSiteDBContext()
        {
        }

        public CSiteDBContext(DbContextOptions<CSiteDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CarProducts> CarProducts { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<ExportProducts> ExportProducts { get; set; }
        public virtual DbSet<ExportReciepts> ExportReciepts { get; set; }
        public virtual DbSet<ImportProducts> ImportProducts { get; set; }
        public virtual DbSet<ImportReciepts> ImportReciepts { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb; database=CSiteDB; integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarProducts>(entity =>
            {
                entity.HasIndex(e => e.CarId, "IX_CarProducts_CarId");

                entity.HasIndex(e => e.ProductId, "IX_CarProducts_ProductId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.CarId).HasColumnName("CarId");

                entity.Property(e => e.ProductId).HasColumnName("ProductId");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarProducts)
                    .HasForeignKey(d => d.CarId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CarProducts)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Cars>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<ExportProducts>(entity =>
            {
                entity.HasIndex(e => e.ExportRecieptId, "IX_ExportProducts_ExportRecieptId");

                entity.HasIndex(e => e.ProductId, "IX_ExportProducts_ProductId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.ExportRecieptId).HasColumnName("ExportRecieptId");

                entity.Property(e => e.ProductId).HasColumnName("ProductId");

                entity.Property(e => e.ReceiptId).HasColumnName("ReceiptId");

                entity.HasOne(d => d.ExportReciept)
                    .WithMany(p => p.ExportProducts)
                    .HasForeignKey(d => d.ExportRecieptId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ExportProducts)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<ExportReciepts>(entity =>
            {
                entity.HasIndex(e => e.CarId, "IX_ExportReciepts_CarId");

                entity.HasIndex(e => e.CustomerId, "IX_ExportReciepts_CustomerId");

                entity.HasIndex(e => e.UserId, "IX_ExportReciepts_UserId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.CarId).HasColumnName("CarId");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.ExportReciepts)
                    .HasForeignKey(d => d.CarId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ExportReciepts)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExportReciepts)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<ImportProducts>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_ImportProducts_ProductId");

                entity.HasIndex(e => e.ReceiptId, "IX_ImportProducts_ReceiptId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.ProductId).HasColumnName("ProductId");

                entity.Property(e => e.ReceiptId).HasColumnName("ReceiptId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ImportProducts)
                    .HasForeignKey(d => d.ProductId);

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ImportProducts)
                    .HasForeignKey(d => d.ReceiptId);
            });

            modelBuilder.Entity<ImportReciepts>(entity =>
            {
                entity.HasIndex(e => e.SupplierId, "IX_ImportReciepts_SupplierId");

                entity.HasIndex(e => e.UserId, "IX_ImportReciepts_UserId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierId");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ImportReciepts)
                    .HasForeignKey(d => d.SupplierId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ImportReciepts)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.AccountId).HasColumnName("AccountId");

                entity.Property(e => e.OperationId).HasColumnName("OperationId");

                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.CarId, "IX_Users_CarId");

                entity.Property(e => e.CarId).HasColumnName("CarId");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CarId);
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
