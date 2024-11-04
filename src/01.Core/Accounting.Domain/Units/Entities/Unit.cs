using Framework.Domain.Entities;

namespace Accounting.Domain.Units.Entities;

public class Unit : AggregateRoot
{
    public int Id { get; set; }
    public string Title { get; set; }
}