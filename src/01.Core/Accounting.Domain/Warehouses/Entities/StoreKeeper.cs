using Accounting.Domain.Warehouses.ValueObjects;

namespace Accounting.Domain.Warehouses.Entities;

public class StoreKeeper
{
    public int Id { get; private set; }
    public string FullName { get; private set; }
    public Phone Phone { get; private set; }
    public string WarehouseId { get; private set; }
    public Warehouse Warehouse { get; private set; }

    private StoreKeeper()
    {
        
    }

    public StoreKeeper(string fullName, Phone phone)
    {
        FullName = fullName;
        Phone = phone;
    }
}