using Accounting.Domain.Warehouses.ValueObjects;

namespace Accounting.Application.Warehouses.Commands
{
    public record AddWarehouseCommand(
        string Name,
        int ProvinceId,
        int CityId,
        string Address,
        string? ImageId,
        IEnumerable<StoreKeeperDto> StoreKeepers);
}