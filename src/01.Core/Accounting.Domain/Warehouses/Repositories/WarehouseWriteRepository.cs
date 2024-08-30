using Accounting.Domain.Warehouses.Entities;
using Framework.Domain;

namespace Accounting.Domain.Warehouses.Repositories
{
    public interface WarehouseWriteRepository : WriteRepository
    {
        void Add(Warehouse warehouse);
        Task<bool> AnyDefaultWarehouse();
        Task<Warehouse> FindAggregate(string id);
    }
}
