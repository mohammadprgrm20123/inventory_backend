using Accounting.Application.Warehouses.Commands;
using Accounting.Domain.Warehouses.Entities;
using Accounting.Domain.Warehouses.Repositories;
using Accounting.TestTools.Configurations;
using FluentAssertions;
using Framework.Domain;
using Framework.Domain.Data;

namespace Accounting.Application.Tests.Warehouses
{
    public class ChangeDefaultWarehouseCommandTests : TestConfig
    {
        private readonly UnitOfWork unitOfWork;
        private readonly WarehouseWriteRepository warehouseWriteRepository;
        private readonly WarehouseReadRepository warehouseReadRepository;
        private readonly ICommandHandler<ChangeDefaultWarehouseCommand, string> handler;

        public ChangeDefaultWarehouseCommandTests()
        {
            unitOfWork = Setup<UnitOfWork>();
            warehouseWriteRepository = Setup<WarehouseWriteRepository>();
            warehouseReadRepository = Setup<WarehouseReadRepository>();
            handler = Setup<ICommandHandler<ChangeDefaultWarehouseCommand, string>>();
        }

        [Fact]
        public async Task ShouldUnsetDefaultWarehouseOnOldWarehouse_WhenNewWarehouseIsExist()
        {
            var firstWarehouse = new Warehouse(
                "dummy-name",
                "dummy-code",
                1,
                2,
                "dummy-address",
                true,
                null);
            warehouseWriteRepository.Add(firstWarehouse);
            var secondWarehouse = new Warehouse(
                "dummy-name",
                "dummy-code",
                3,
                4,
                "dummy-address",
                false,
                null);
            warehouseWriteRepository.Add(secondWarehouse);
            await unitOfWork.Complete();
            var command = new ChangeDefaultWarehouseCommand(secondWarehouse.Id);

            await handler.Handle(command);

            var actualResult = warehouseReadRepository
                .GetAll(new()).Result.First(q=>q.Id == firstWarehouse.Id);
            actualResult.IsDefault.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNewWarehouseAsDefaultWarehouse_WhenNewWarehouseIsExist()
        {
            var firstWarehouse = new Warehouse(
                "dummy-name",
                "dummy-code",
                5,
                6,
                "dummy-address",
                true,
                null);
            warehouseWriteRepository.Add(firstWarehouse);
            var secondWarehouse = new Warehouse(
                "dummy-name",
                "dummy-code",
                7,
                8,
                "dummy-address",
                false,
                null);
            warehouseWriteRepository.Add(secondWarehouse);
            await unitOfWork.Complete();
            var command = new ChangeDefaultWarehouseCommand(secondWarehouse.Id);

            await handler.Handle(command);

            var actualResult = warehouseReadRepository
                .GetAll(new()).Result.First(q => q.Id == secondWarehouse.Id);
            actualResult.IsDefault.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnError_WhenNewWarehouseNotExist()
        {
            var firstWarehouse = new Warehouse(
                "dummy-name",
                "dummy-code",
                7,
                6,
                "dummy-address",
                true,
                null);
            warehouseWriteRepository.Add(firstWarehouse);
            await unitOfWork.Complete();
            var command = new ChangeDefaultWarehouseCommand(Guid.NewGuid().ToString());

            var actualResult = await handler.Handle(command);

            actualResult.IsError.Should().BeTrue();
        }
    }
}
