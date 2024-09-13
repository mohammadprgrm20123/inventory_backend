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
            builder.Property(q => q.Address).IsRequired();
            builder.Property(q => q.ProvinceId).IsRequired();
            builder.Property(q => q.CityId).IsRequired();
            builder.Property(q => q.Code).IsRequired();
            builder.Property(q => q.Name).IsRequired();

            builder.OwnsOne(q => q.Avatar, p =>
            {
                p.Property(v => v.Id).IsRequired(false).HasColumnName("AvatarId");
                p.Property(v => v.Extension).IsRequired(false).HasColumnName("AvatarExtension");
            });
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
                p.Property(v => v.CountryCallingCode).IsRequired().HasColumnName("CountryCallingCode");
                p.Property(v => v.PhoneNumber).IsRequired().HasColumnName("PhoneNumber");
            });

            builder
                .HasOne(q => q.Warehouse)
                .WithMany(q => q.StoreKeepers)
                .HasForeignKey(q => q.WarehouseId);
        }
    }
}
