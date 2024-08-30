using Accounting.Domain.Warehouses.ValueObjects;

namespace Accounting.Application.Warehouses.Commands
{
    public record AddWarehouseCommand(
        string Name,
        int ProvinceId,
        int CityId,
        string Address,
        Avatar? Avatar,
        List<AddStoreKeeperDto> StoreKeepers);

    public record AddStoreKeeperDto(string FullName, Phone Phone);
}
