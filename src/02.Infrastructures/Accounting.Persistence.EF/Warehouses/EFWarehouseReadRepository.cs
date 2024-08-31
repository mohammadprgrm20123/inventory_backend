using Accounting.Domain.Warehouses.Entities;
using Accounting.Domain.Warehouses.Repositories;
using Accounting.Domain.Warehouses.Repositories.ViewModels;
using Framework.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Persistence.EF.Warehouses
{
    public class EFWarehouseReadRepository(ApplicationReadDbContext context) : WarehouseReadRepository
    {
        public async Task<IEnumerable<GetAllWarehouseViewModel>> GetAll(Pagination pagination)
        {
            return await context
                .Set<Warehouse>()
                .Select(q => new GetAllWarehouseViewModel(
                    q.Id,
                    q.Name,
                    q.IsDefault,
                    q.Address,
                    q.StoreKeepers.Any() ? q.StoreKeepers.FirstOrDefault()!.Phone : null,
                    q.Avatar
                ))
                .Skip((pagination.Offset - 1) * pagination.Limit)
                .Take(pagination.Limit)
                .ToListAsync();
        }

        public async Task<GetWarehouseByIdViewModel?> GetWarehouseById(string warehouseId)
        {
            return await context
                .Set<Warehouse>()
                .Where(q => q.Id == warehouseId)
                .Select(q => new GetWarehouseByIdViewModel(
                    q.Name,
                    q.Code,
                    q.Address,
                    q.Avatar,
                    q.StoreKeepers
                        .Select(s => new StoreKeeperViewModel(
                        s.Id,
                        s.FullName,
                        s.Phone))
                    )
                ).FirstOrDefaultAsync();
        }
    }
}
