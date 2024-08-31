using Accounting.Application.Warehouses.Commands;
using Accounting.Domain.Warehouses.Entities;
using Accounting.Domain.Warehouses.Repositories;
using Accounting.Domain.Warehouses.ValueObjects;
using Accounting.TestTools.Configurations;
using FluentAssertions;
using Framework.Domain;
using Framework.Domain.Data;

namespace Accounting.Application.Tests.Warehouses
{
    public class AddWarehouseCommandHandlerTests : TestConfig
    {
        private readonly ICommandHandler<AddWarehouseCommand, string> _handler;
        private readonly WarehouseWriteRepository repository;
        private readonly UnitOfWork unitOfWork;

        public AddWarehouseCommandHandlerTests()
        {
            _handler = Setup<ICommandHandler<AddWarehouseCommand, string>>();
            repository = Setup<WarehouseWriteRepository>();
            unitOfWork = Setup<UnitOfWork>();
        }

        [Fact]
        public async Task ShouldAddWarehouseWithIsDefaultValueTrue_WhenDoesNotExistAnotherWarehouse()
        {
            var command = new AddWarehouseCommand(
                "dummy-name",
                1,
                2,
                "dummy-address",
                new Avatar("dummy-id", ".jpg"),
                new());

            var handlerResult = await _handler.Handle(command);

            var actualResult = await repository
                .FindAggregate(handlerResult.Value);
            actualResult.Name.Should().Be(command.Name);
            actualResult.Address.Should().Be(command.Address);
            actualResult.CityId.Should().Be(command.CityId);
            actualResult.ProvinceId.Should().Be(command.ProvinceId);
            actualResult.Code.Should().NotBeNull();
            actualResult.IsDefault.Should().BeTrue();
            actualResult.Avatar.Should().NotBeNull();
            actualResult.Avatar!.Id.Should().Be(command.Avatar.Id);
            actualResult.Avatar.Extension.Should().Be(command.Avatar.Extension);
        }

        [Fact]
        public async Task ShouldAddWarehouseWithIsDefaultValueFalse_WhenExistAnotherWarehouse()
        {
            var warehouse = new Warehouse(
                "dummy",
                "dummy",
                1,
                2,
                "dummy",
                true,
                null);
            repository.Add(warehouse);
            await unitOfWork.Complete();
            var command = new AddWarehouseCommand(
                "dummy-name",
                1,
                2,
                "dummy-address",
                new Avatar("dummy-id", ".jpg"),
                new());

            var handlerResult = await _handler.Handle(command);

            var actualResult = await repository
                .FindAggregate(handlerResult.Value);
            actualResult.Name.Should().Be(command.Name);
            actualResult.Address.Should().Be(command.Address);
            actualResult.CityId.Should().Be(command.CityId);
            actualResult.ProvinceId.Should().Be(command.ProvinceId);
            actualResult.Code.Should().NotBeNull();
            actualResult.IsDefault.Should().BeFalse();
            actualResult.Avatar.Should().NotBeNull();
            actualResult.Avatar!.Id.Should().Be(command.Avatar!.Id);
            actualResult.Avatar.Extension.Should().Be(command.Avatar.Extension);
        }

        [Fact]
        public async Task ShouldAddStoreKeeper_properly()
        {
            var command = new AddWarehouseCommand(
                "dummy-name",
                1,
                2,
                "dummy-address",
                new Avatar("dummy-id", ".jpg"),
                new() { new("Hassan Rezaei", new Phone("0098", "0987123452")) });

            var handlerResult = await _handler
                .Handle(command);

            var actualResult = await repository
                .FindAggregate(handlerResult.Value);
            actualResult.StoreKeepers.Should().HaveCount(1);
            actualResult.StoreKeepers.First().FullName.Should().Be(command.StoreKeepers.First().FullName);
            actualResult.StoreKeepers.First().Phone.Should()
                .BeEquivalentTo(command.StoreKeepers.First().Phone);
        }
    }
}
