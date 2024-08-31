using Accounting.Domain.Warehouses.Entities;
using Accounting.Domain.Warehouses.Repositories;
using Framework.Domain.Entities;
using Framework.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Persistence.EF.Warehouses
{
    public class EFWarehouseWriteRepository(
        ApplicationWriteDbContext context,
        IOutboxManagement outboxManagement) : WarehouseWriteRepository
    {
        public void Add(Warehouse warehouse)
        {
            context.Add(warehouse);
            RaiseEvent(warehouse);
        }

        public async Task<Warehouse?> FindWarehouse(string id)
        {
            return await context
                .Set<Warehouse>()
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Warehouse?> FindDefaultWarehouse()
        {
            return await context
                .Set<Warehouse>()
                .FirstOrDefaultAsync(q => q.IsDefault == true);
        }

        public async Task<bool> AnyDefaultWarehouse()
        {
            return await context
                .Set<Warehouse>()
                .AnyAsync(q => q.IsDefault == true);
        }

        public async Task<Warehouse> FindAggregate(string id)
        {
            return await context
                .Set<Warehouse>()
                .Include(q => q.StoreKeepers)
                .FirstAsync(q => q.Id == id);
        }

        public void RaiseEvent(AggregateRoot entity)
        {
            outboxManagement.Add(entity);
        }
    }
}
