using Accounting.Domain.Warehouses.ValueObjects;

namespace Accounting.Application.Warehouses.Commands
{
    public record AddWarehouseCommand(
        string Name,
        string ProvinceName,
        string CityName,
        string Address,
        Avatar? Avatar,
        IEnumerable<StoreKeeperDto> StoreKeepers);
}