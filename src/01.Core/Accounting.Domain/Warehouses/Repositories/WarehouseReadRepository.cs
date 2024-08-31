using Accounting.Domain.Warehouses.Repositories.ViewModels;
using Framework.Domain.Data;

namespace Accounting.Domain.Warehouses.Repositories
{
    public interface WarehouseReadRepository : ReadRepository
    {
        Task<IEnumerable<GetAllWarehouseViewModel>> GetAll(Pagination pagination);
    }
}
