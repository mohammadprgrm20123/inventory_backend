using System.Security.AccessControl;
using Accounting.Domain.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Persistence.EF.Products;

public class ProductEntityMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Name).IsRequired();
        builder.Property(q => q.Code).HasMaxLength(100).IsRequired();
        builder.Property(q => q.Barcode).IsRequired();
    }
}

public class ProductCategoryEntityMap : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategories");
        builder.HasKey(q => new { q.CategoryId, q.ProductId });

        builder.HasOne(q => q.Product)
            .WithMany(q => q.Categories)
            .HasForeignKey(q => q.ProductId);
    }
}

public class ProductUnitEntityMap : IEntityTypeConfiguration<ProductUnit>
{
    public void Configure(EntityTypeBuilder<ProductUnit> builder)
    {
        builder.ToTable("ProductUnits");
        builder.HasKey(q => new { q.UnitId, q.ProductId });

        builder.HasOne(q => q.Product)
            .WithMany(q => q.Units)
            .HasForeignKey(q => q.ProductId);
    }
}

public class ProductAttributeEntityMap : IEntityTypeConfiguration<ProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        builder.ToTable("ProductAttributes");
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Title).IsRequired();
        builder.Property(q => q.Type).IsRequired();
        builder.Property(q => q.Value).IsRequired();

        builder.HasOne(q => q.Product)
            .WithMany(q => q.Attributes)
            .HasForeignKey(q => q.ProductId);
    }
}