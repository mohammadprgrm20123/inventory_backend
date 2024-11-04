using System.Reflection.Metadata;
using Framework.Domain.Entities;

namespace Accounting.Domain.Products.Entities;

public class Product : AggregateRoot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Barcode { get; set; }
    public HashSet<ProductCategory> Categories { get; set; } = new();
    public HashSet<ProductUnit> Units { get; set; } = new();
    public HashSet<ProductAttribute> Attributes { get; set; } = new();
}