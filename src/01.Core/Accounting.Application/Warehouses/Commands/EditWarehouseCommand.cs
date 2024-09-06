using Accounting.Domain.Warehouses.ValueObjects;

namespace Accounting.Application.Warehouses.Commands;

public record EditWarehouseCommand(
    string WarehouseId,
    string Name,
    string ProvinceName,
    string CityName,
    string Address,
    Avatar? Avatar,
    IEnumerable<StoreKeeperDto> StoreKeepers);
    
public record StoreKeeperDto(string FullName, Phone Phone);