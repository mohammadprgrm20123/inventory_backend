using Accounting.Application.Warehouses.Commands;
using Accounting.Domain.Warehouses.Entities;
using Accounting.Domain.Warehouses.Repositories;
using Accounting.Persistence.EF.Warehouses;
using Accounting.TestTools.Configurations;
using FluentAssertions;
using Framework.Domain;
using Framework.Domain.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace Accounting.Application.Tests.Warehouses;

public class DeleteWarehouseCommandHandlerTests : TestConfig
{
    private readonly ICommandHandler<DeleteWarehouseCommand, string> sut;
    private readonly WarehouseWriteRepository writeRepository;
    private readonly WarehouseReadRepository readRepository;
    private readonly UnitOfWork unitOfWork;

    public DeleteWarehouseCommandHandlerTests()
    {
        sut = Setup<ICommandHandler<DeleteWarehouseCommand, string>>();
        writeRepository = Setup<WarehouseWriteRepository>();
        readRepository = Setup<WarehouseReadRepository>();
        unitOfWork = Setup<UnitOfWork>();
    }

    [Fact]
    public async Task DeleteWarehouse_whenSentDeleteWarehouseCommand()
    {
        var warehouse = new Warehouse(
            "dummy-name",
            "123",
            1,
            2,
            "dummy-address",
            true,
            null);
        writeRepository.Add(warehouse);
        await unitOfWork.Complete();
        var command = new DeleteWarehouseCommand(warehouse.Id);

        await sut.Handle(command);

        var actualResult = await readRepository
            .GetWarehouseById(warehouse.Id);
        actualResult.Should().BeNull();
    }

    [Fact]
    public async Task PreventDeleteWarehouse_WhenWarehouseNotExist()
    {
        var command = new DeleteWarehouseCommand(Guid.NewGuid().ToString());

        var actualResult = await sut.Handle(command);

        actualResult.IsError.Should().BeTrue();
        actualResult.FirstError.Code.Should().Be("404");
    }
}