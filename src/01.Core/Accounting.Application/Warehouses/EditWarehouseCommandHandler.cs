using Accounting.Application.Warehouses.Commands;
using Accounting.Domain.Warehouses.Repositories;
using ErrorOr;
using Framework.Domain;
using Framework.Domain.Data;

namespace Accounting.Application.Warehouses;

public class EditWarehouseCommandHandler(
    UnitOfWork unitOfWork,
    WarehouseWriteRepository repository)
    : ICommandHandler<EditWarehouseCommand, string>
{
    public async Task<ErrorOr<string>> Handle(EditWarehouseCommand command)
    {
        var warehouse = await repository
            .FindWithStoreKeepers(command.WarehouseId);

        if (warehouse is null)
            return Error.NotFound("500", "انبار پیدا نشد");

        warehouse.Edit(
            command.Name,
            command.Address,
            command.ProvinceId,
            command.CityId,
            command.ImageId);

        warehouse.RemoveAllStoreKeeper();

        foreach (var storeKeeper in command.StoreKeepers)
        {
            warehouse.AddStoreKeeper(storeKeeper.FullName, storeKeeper.Phone);
        }

        repository.Update(warehouse);
        await unitOfWork.Complete();

        return command.WarehouseId;
    }
}