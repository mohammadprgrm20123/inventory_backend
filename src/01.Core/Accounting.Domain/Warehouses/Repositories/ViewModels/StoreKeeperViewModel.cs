using Accounting.Domain.Warehouses.ValueObjects;

namespace Accounting.Domain.Warehouses.Repositories.ViewModels;

public record StoreKeeperViewModel(
    int Id,
    string FullName,
    Phone Phone);