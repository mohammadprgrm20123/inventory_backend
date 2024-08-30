using Accounting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Persistence.EF.Documents
{
    internal class DocumentEntityMap : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Data).IsRequired();
            builder.Property(q => q.Extension).IsRequired();
            builder.Property(q => q.CreationDate).IsRequired();
            builder.Property(q => q.Status).IsRequired();
        }
    }
}
