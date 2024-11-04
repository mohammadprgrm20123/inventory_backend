using Accounting.Domain.Products.Entities;
using Framework.Domain.Entities;

namespace Accounting.Domain.DefaultAttibutes.Entities;

public class DefaultAttribute : AggregateRoot
{
    public int Id { get; set; }
    public string Title { get; set; }
    public AttributeType Type { get; set; }
}

public enum AttributeType : byte
{
    Text = 1,
    Number = 2,
    Date = 3,
    Color = 4,
    Switch = 5
}