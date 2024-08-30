using Accounting.Domain;
using Framework.Domain.Entities;
using Framework.Domain.Events;
using Newtonsoft.Json;

namespace Accounting.Persistence.EF.OutboxMessages
{
    public class OutboxManagement(ApplicationWriteDbContext context) : IOutboxManagement
    {
        public void Add(AggregateRoot aggregateRoot)
        {
            var events = aggregateRoot.Events;

            foreach (var domainEvent in events)
            {
                var outboxMessage = new OutboxMessage(
                    domainEvent.GetType().FullName!,
                    JsonConvert.SerializeObject(domainEvent));

                context.Set<OutboxMessage>().Add(outboxMessage);
            }
        }
    }
}
