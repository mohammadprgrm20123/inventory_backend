using System.Data;
using FluentMigrator;

namespace Accounting.Migrations.Migrations
{
    [Migration(202408302013)]
    public class _202408302013_AddWarehouseTable : Migration
    {
        public override void Up()
        {
            Create.Table("Warehouses")
                .WithColumn("Id").AsCustom("VARCHAR(100)").PrimaryKey().NotNullable()
                .WithColumn("Name").AsCustom("NVARCHAR(100)").NotNullable()
                .WithColumn("Code").AsCustom("VARCHAR(10)").NotNullable()
                .WithColumn("ProvinceName").AsString(450).NotNullable()
                .WithColumn("CityName").AsString(450).NotNullable()
                .WithColumn("Address").AsString(1000).NotNullable()
                .WithColumn("AvatarId").AsCustom("VARCHAR(100)").Nullable()
                .WithColumn("AvatarExtension").AsCustom("VARCHAR(10)").Nullable()
                .WithColumn("IsDefault").AsBoolean().NotNullable();

            Create.Table("StoreKeepers")
                .WithColumn("Id").AsInt32().Identity().NotNullable()
                .WithColumn("FullName").AsString(450).NotNullable()
                .WithColumn("CountryCallingCode").AsCustom("VARCHAR(5)")
                .WithColumn("PhoneNumber").AsCustom("VARCHAR(15)").NotNullable()
                .WithColumn("WarehouseId").AsCustom("VARCHAR(100)").NotNullable()
                .ForeignKey("FK_StoreKeepers_Warehouses", "Warehouses", "Id")
                .OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("StoreKeepers");
            Delete.Table("Warehouses");
        }
    }
}
