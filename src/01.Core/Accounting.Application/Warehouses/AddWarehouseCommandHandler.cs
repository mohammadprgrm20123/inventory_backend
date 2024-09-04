using Accounting.Application.Warehouses.Commands;
using Accounting.Domain.Warehouses.Entities;
using Accounting.Domain.Warehouses.Repositories;
using ErrorOr;
using Framework.Domain;
using Framework.Domain.Data;

namespace Accounting.Application.Warehouses
{
    public class AddWarehouseCommandHandler(
        UnitOfWork unitOfWork,
        WarehouseWriteRepository repository) : ICommandHandler<AddWarehouseCommand, string>
    {
        public async Task<ErrorOr<string>> Handle(AddWarehouseCommand command)
        {
            var anyWarehouse = await repository.AnyDefaultWarehouse();
            var code = command.Name[..3] + "-" + Guid.NewGuid().ToString()[..5];

            var warehouse = new Warehouse(
                command.Name,
                code,
                command.ProvinceName,
                command.CityName,
                command.Address,
                !anyWarehouse,
                command.Avatar);

            foreach (var storeKeeper in command.StoreKeepers)
            {
                warehouse.AddStoreKeeper(
                    storeKeeper.FullName,
                    storeKeeper.Phone);
            }

            repository.Add(warehouse);
            await unitOfWork.Complete();

            return warehouse.Id;
        }
    }
}
