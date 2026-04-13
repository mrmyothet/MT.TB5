using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TB5.NLayer.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductCategory> TblProductCategories { get; set; }

    public virtual DbSet<TblSale> TblSales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MYOTHETPC\\MSSQLEXPRESS2022;Database=Batch5MiniPOS;User ID=sa;Password=admin123!;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.ToTable("Tbl_Product");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId);

            entity.ToTable("Tbl_ProductCategory");

            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ProductCategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblSale>(entity =>
        {
            entity.HasKey(e => e.SaleId);

            entity.ToTable("Tbl_Sale");

            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SaleDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
