using Accounting.Application.Warehouses.Commands;
using Accounting.Domain.Warehouses.Repositories;
using ErrorOr;
using Framework.Domain;
using Framework.Domain.Data;

namespace Accounting.Application.Warehouses;

public class DeleteWarehouseCommandHandler(
    WarehouseWriteRepository repository)
    : ICommandHandler<DeleteWarehouseCommand, string>
{
    public async Task<ErrorOr<string>> Handle(DeleteWarehouseCommand command)
    {
        if (await repository.IsExist(command.Id) is false)
            return Error.NotFound("404", "انبار پیدا نشد");

        await repository
            .DeleteWarehouse(command.Id);

        return command.Id;
    }
}