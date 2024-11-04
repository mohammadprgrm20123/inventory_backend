using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using FluentMigrator;

namespace Accounting.Migrations.Migrations;

[Migration(202411042023)]
public class _202411042023_CreateProductTables : Migration
{
    public override void Up()
    {
        Create.Table("Products")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(500).NotNullable()
            .WithColumn("Code").AsString(100).NotNullable()
            .WithColumn("Barcode").AsString().NotNullable();

        Create.Table("ProductCategories")
            .WithColumn("CategoryId").AsInt32().NotNullable()
            .WithColumn("ProductId").AsInt32().NotNullable()
            .ForeignKey("FK_ProductCategory_Products", "Products", "Id");
        
        Create.Table("ProductUnits")
            .WithColumn("UnitId").AsInt32().NotNullable()
            .WithColumn("ProductId").AsInt32().NotNullable()
            .ForeignKey("FK_ProductUnits_Products", "Products", "Id");
        
        Create.Table("ProductAttributes")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(100).NotNullable()
            .WithColumn("Type").AsByte().NotNullable()
            .WithColumn("Value").AsString().NotNullable()
            .WithColumn("ProductId").AsInt32().NotNullable()
            .ForeignKey("FK_ProductAttributes_Products", "Products", "Id");
        
        Create.Table("DefaultAttributes")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(100).NotNullable()
            .WithColumn("Type").AsByte().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("ProductAttributes");
        Delete.Table("ProductUnits");
        Delete.Table("ProductCategories");
        Delete.Table("Products");
        Delete.Table("DefaultAttributes");
    }
}