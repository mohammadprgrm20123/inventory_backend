using Accounting.Domain.Warehouses.ValueObjects;
using Framework.Domain.Entities;

namespace Accounting.Domain.Warehouses.Entities
{
    public class Warehouse : AggregateRoot
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public int ProvinceId { get; private set; }
        public int CityId { get; private set; }
        public string Address { get; private set; }
        public bool IsDefault { get; private set; }
        public string? ImageId { get; private set; }

        private HashSet<StoreKeeper> _storeKeepers = new();
        public IReadOnlyCollection<StoreKeeper> StoreKeepers => _storeKeepers;

        private Warehouse()
        {
        }

        public Warehouse(
            string name,
            string code,
            int provinceId,
            int cityId,
            string address,
            bool isDefault,
            string imageId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Code = code;
            ProvinceId = provinceId;
            CityId = cityId;
            Address = address;
            IsDefault = isDefault;
            ImageId = imageId;
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
            int provinceName,
            int cityName,
            string? imageId)
        {
            Name = name;
            Address = address;
            ProvinceId = provinceName;
            CityId = cityName;
            ImageId = imageId;
        }

        public void RemoveAllStoreKeeper()
        {
            _storeKeepers = new();
        }
    }
}