using Accounting.Domain.Warehouses.ValueObjects;

namespace Accounting.Application.Warehouses.Commands;

public record EditWarehouseCommand(
    string WarehouseId,
    string Name,
    int ProvinceId,
    int CityId,
    string Address,
    string? ImageId,
    IEnumerable<StoreKeeperDto> StoreKeepers);
    
public record StoreKeeperDto(string FullName, Phone? Phone);