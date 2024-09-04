using Accounting.Domain.Warehouses.Entities;
using Accounting.Domain.Warehouses.Repositories;
using Accounting.Domain.Warehouses.ValueObjects;
using Accounting.TestTools.Configurations;
using FluentAssertions;
using Framework.Domain.Data;
using Hangfire.Annotations;

namespace Accounting.Persistence.Tests.Warehouses
{
    public class WarehouseReadRepositoryTest : TestConfig
    {
        private readonly UnitOfWork unitOfWork;
        private readonly WarehouseReadRepository readRepository;
        private readonly WarehouseWriteRepository writeRepository;

        public WarehouseReadRepositoryTest()
        {
            unitOfWork = Setup<UnitOfWork>();
            readRepository = Setup<WarehouseReadRepository>();
            writeRepository = Setup<WarehouseWriteRepository>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnWarehouse_whenExistWarehouse()
        {
            var warehouse = new Warehouse(
                "dummy-name",
                "dummy-code",
                "dummy-province",
                "dummy-city",
                "dummy-address",
                true,
                new Avatar("avatarId", ".jpg"));
            warehouse.AddStoreKeeper("hassan", new Phone("0098", "09875674321"));
            writeRepository.Add(warehouse);
            await unitOfWork.Complete();

            var actualResult = await readRepository.GetAll(new Pagination());

            actualResult.Should().HaveCount(1);
            actualResult.First().Id.Should().Be(warehouse.Id);
            actualResult.First().Name.Should().Be(warehouse.Name);
            actualResult.First().Address.Should().Be(warehouse.Address);
            actualResult.First().Avatar.Should().BeEquivalentTo(warehouse.Avatar);
            actualResult.First().Phone.Should()
                .BeEquivalentTo(warehouse.StoreKeepers.First().Phone);
        }

        [Fact]
        public async Task GetById_ShouldReturnWarehouse_whenWarehouseIdIsValid()
        {
            var warehouse = new Warehouse(
                "dummy-name",
                "dummy-code",
                "dummy-province",
                "dummy-city",
                "dummy-address",
                true,
                new Avatar("avatarId", ".jpg"));
            warehouse.AddStoreKeeper("hassan", new Phone("0098", "09875674321"));
            writeRepository.Add(warehouse);
            await unitOfWork.Complete();

            var actualResult = await readRepository.GetWarehouseById(warehouse.Id);
            actualResult.Should().NotBeNull();
            actualResult!.name.Should().Be(warehouse.Name);
            actualResult.address.Should().Be(warehouse.Address);
            actualResult.code.Should().Be(warehouse.Code);
            actualResult.Avatar.Should().BeEquivalentTo(warehouse.Avatar);
            actualResult.StoreKeepers.Should().HaveCount(1);
        }
    }
}
