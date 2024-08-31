using Framework.Domain.Entities;

namespace Framework.Domain.Data;

public interface WriteRepository
{
    void RaiseEvent(AggregateRoot entity);
}