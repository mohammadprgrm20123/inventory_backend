using Accounting.Domain.Warehouses.ValueObjects;
using Framework.Domain.Entities;

namespace Accounting.Domain.Warehouses.Entities
{
    public class Warehouse : AggregateRoot
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string ProvinceName { get; private set; }
        public string CityName { get; private set; }
        public string Address { get; private set; }
        public bool IsDefault { get; private set; }
        public Avatar? Avatar { get; private set; }

        private HashSet<StoreKeeper> _storeKeepers = new();
        public IReadOnlyCollection<StoreKeeper> StoreKeepers => _storeKeepers;

        private Warehouse()
        {
        }

        public Warehouse(
            string name,
            string code,
            string provinceName,
            string cityName,
            string address,
            bool isDefault,
            Avatar? avatar)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Code = code;
            ProvinceName = provinceName;
            CityName = cityName;
            Address = address;
            IsDefault = isDefault;
            Avatar = avatar;
        }

        public void AddStoreKeeper(string fullName, Phone phone)
        {
            var storeKeeper = new StoreKeeper(fullName, phone);
            _storeKeepers.Add(storeKeeper);
        }

        public void RemoveAsDefaultWarehouse()
        {
            IsDefault = false;
        }

        public void SetAsDefaultWarehouse()
        {
            IsDefault = true;
        }

        public void Edit(
            string name,
            string address,
            string provinceName,
            string cityName,
            Avatar? avatar)
        {
            Name = name;
            Address = address;
            ProvinceName = provinceName;
            CityName = cityName;
            Avatar = avatar;
        }

        public void RemoveAllStoreKeeper()
        {
            _storeKeepers = new();
        }
    }
}