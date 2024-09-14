using Accounting.Domain.Warehouses.ValueObjects;

namespace Accounting.Domain.Warehouses.Repositories.ViewModels
{
    public record GetAllWarehouseViewModel(
        string Id,
        string Name,
        bool IsDefault,
        string Address,
        Phone? Phone,
        string? ImageId);
}
