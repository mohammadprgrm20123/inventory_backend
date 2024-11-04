using Accounting.Domain.DefaultAttibutes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Persistence.EF.DefaultAttributes;

public class DefaultAttributeEntityMap : IEntityTypeConfiguration<DefaultAttribute>
{
    public void Configure(EntityTypeBuilder<DefaultAttribute> builder)
    {
        builder.ToTable("DefaultAttributes");
        builder.Property(q => q.Id);
        builder.Property(q => q.Title).IsRequired();
        builder.Property(q => q.Type).IsRequired();
    }
}