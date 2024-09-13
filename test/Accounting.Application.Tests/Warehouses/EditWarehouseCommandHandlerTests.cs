using Accounting.Application.Warehouses.Commands;
using Accounting.Domain.Warehouses.Entities;
using Accounting.Domain.Warehouses.Repositories;
using Accounting.Domain.Warehouses.ValueObjects;
using Accounting.TestTools.Configurations;
using ErrorOr;
using FluentAssertions;
using Framework.Domain;
using Framework.Domain.Data;

namespace Accounting.Application.Tests.Warehouses;

public class EditWarehouseCommandHandlerTests : TestConfig
{
    private readonly UnitOfWork unitOfWork;
    private readonly WarehouseWriteRepository warehouseWriteRepository;
    private readonly WarehouseReadRepository warehouseReadRepository;
    private readonly ICommandHandler<EditWarehouseCommand, string> handler;

    public EditWarehouseCommandHandlerTests()
    {
        unitOfWork = Setup<UnitOfWork>();
        warehouseWriteRepository = Setup<WarehouseWriteRepository>();
        warehouseReadRepository = Setup<WarehouseReadRepository>();
        handler = Setup<ICommandHandler<EditWarehouseCommand, string>>();
    }

    [Fact]
    public async Task EditWarehouse_properly()
    {
        var warehouse = new Warehouse(
            "dummy_name",
            "dummy_code",
            1,
            2,
            "dummy-address",
            true,
            new());
        warehouseWriteRepository.Add(warehouse);
        await unitOfWork.Complete();
        var command = new EditWarehouseCommand(
            warehouse.Id,
            "name-edited",
            3,
            4,
            "address-edited",
            new(),
            new List<StoreKeeperDto>());

        await handler.Handle(command);

        var actualResult = await warehouseReadRepository
            .GetWarehouseById(warehouse.Id);
        actualResult!.name.Should().Be(command.Name);
        actualResult.address.Should().Be(command.Address);
        actualResult.code.Should().Be(warehouse.Code);
    }

    [Fact]
    public async Task EditStoreKeeper_properly()
    {
        var warehouse = new Warehouse(
            "dummy_name",
            "dummy_code",
            1,
            2,
            "dummy-address",
            true,
            new());
        warehouse.AddStoreKeeper("hassan", new("0098", "9016785432"));
        warehouseWriteRepository.Add(warehouse);
        await unitOfWork.Complete();
        var command = new EditWarehouseCommand(
            warehouse.Id,
            "name-edited",
            3,
            4,
            "address-edited",
            new(),
            new List<StoreKeeperDto>
                { new("ali", new Phone("0098", "09179875643")) }
        );
        var expectedResult = command.StoreKeepers.First();

        await handler.Handle(command);

        var result = await warehouseReadRepository
            .GetWarehouseById(warehouse.Id);
        result!.StoreKeepers.Should().HaveCount(1);
        var actualResult = result.StoreKeepers.First();
        actualResult.FullName.Should().Be(expectedResult.FullName);
        actualResult.Phone.Should().BeEquivalentTo(expectedResult.Phone);
    }

    [Fact]
    public async Task PreventEditWarehouse_WhenWarehouseNotFound()
    {
        var command = new EditWarehouseCommand(
            Guid.NewGuid().ToString(),
            "name-edited",
            1,
            2,
            "address-edited",
            new(),
            new List<StoreKeeperDto>());

        var actualResult = await handler.Handle(command);

        actualResult.Errors.Should().HaveCount(1);
        actualResult.Errors.First().Type.Should().Be(ErrorType.NotFound);
    }
}