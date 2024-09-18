using Accounting.Domain.Warehouses.ValueObjects;

namespace Accounting.Domain.Warehouses.Repositories.ViewModels
{
    public record GetWarehouseByIdViewModel(
        string Name,
        string Code,
        string Address,
        int ProvinceId,
        int CityId,
        bool IsDefault,
        string? ImageId,
        IEnumerable<StoreKeeperViewModel> StoreKeepers);
}
