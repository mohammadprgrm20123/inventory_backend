using Accounting.Domain.Warehouses.Entities;
using Framework.Domain.Data;

namespace Accounting.Domain.Warehouses.Repositories
{
    public interface WarehouseWriteRepository : WriteRepository
    {
        void Add(Warehouse warehouse);
        void Update(Warehouse warehouse);
        Task<Warehouse?> FindWarehouse(string id);
        Task<Warehouse> FindAggregate(string id);
        Task<Warehouse?> FindDefaultWarehouse();
        Task<bool> AnyDefaultWarehouse();
    }
}
