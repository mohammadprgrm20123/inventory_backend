using Framework.Domain.Entities;

namespace Accounting.Domain.Categories.Entities;

public class Category : AggregateRoot
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? ParentId { get; set; }
}