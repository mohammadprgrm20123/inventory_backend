using Accounting.Domain.Warehouses.ValueObjects;

namespace Accounting.Domain.Warehouses.Repositories.ViewModels
{
    public record GetWarehouseByIdViewModel(
        string name,
        string code,
        string address,
        string? imageId,
        IEnumerable<StoreKeeperViewModel> StoreKeepers);
}
