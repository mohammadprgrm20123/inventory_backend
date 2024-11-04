using Accounting.Domain.DefaultAttibutes.Entities;

namespace Accounting.Domain.Products.Entities;

public class ProductAttribute
{
    public int Id { get; set; }
    public string Title { get; set; }
    public AttributeType Type { get; set; }
    public string Value { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}