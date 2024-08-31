using Accounting.Application.Warehouses.Commands;
using Accounting.Domain.Warehouses.Repositories;
using ErrorOr;
using Framework.Domain;
using Framework.Domain.Data;

namespace Accounting.Application.Warehouses
{
    public class ChangeDefaultWarehouseCommandHandler(
        UnitOfWork unitOfWork,
        WarehouseWriteRepository repository) : ICommandHandler<ChangeDefaultWarehouseCommand, string>
    {
        public async Task<ErrorOr<string>> Handle(ChangeDefaultWarehouseCommand command)
        {
            var currentDefaultWarehouse = await repository
                .FindDefaultWarehouse();
            var newWarehouse = await repository
                .FindWarehouse(command.warehouseId);

            if (newWarehouse is null)
                return Error.NotFound("500", "انبار پیدا نشد");

            if (currentDefaultWarehouse != null)
                currentDefaultWarehouse
                    .RemoveAsDefaultWarehouse();

            newWarehouse.SetAsDefaultWarehouse();

            await unitOfWork.Complete();

            return command.warehouseId;
        }
    }
}
