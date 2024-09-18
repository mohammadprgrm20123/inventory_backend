using Accounting.Domain.Warehouses.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Persistence.EF.Warehouses
{
    public class WarehouseEntityMap : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.ImageId).IsRequired(false);
            builder.Property(q => q.Address).IsRequired();
            builder.Property(q => q.ProvinceId).IsRequired();
            builder.Property(q => q.CityId).IsRequired();
            builder.Property(q => q.Code).IsRequired();
            builder.Property(q => q.Name).IsRequired();
        }
    }

    public class StoreKeeperEntityMap : IEntityTypeConfiguration<StoreKeeper>
    {
        public void Configure(EntityTypeBuilder<StoreKeeper> builder)
        {
            builder.ToTable("StoreKeepers");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.FullName).IsRequired();

            builder.OwnsOne(q => q.Phone, p =>
            {
                p.Property(v => v.CountryCallingCode).IsRequired(false).HasColumnName("CountryCallingCode");
                p.Property(v => v.PhoneNumber).IsRequired(false).HasColumnName("PhoneNumber");
            });

            builder
                .HasOne(q => q.Warehouse)
                .WithMany(q => q.StoreKeepers)
                .HasForeignKey(q => q.WarehouseId);
        }
    }
}
