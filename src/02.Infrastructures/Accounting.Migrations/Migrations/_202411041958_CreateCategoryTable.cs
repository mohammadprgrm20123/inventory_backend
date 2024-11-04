using FluentMigrator;

namespace Accounting.Migrations.Migrations;

[Migration(202411041958)]
public class _202411041958_CreateCategoryTable : Migration
{
    public override void Up()
    {
        Create.Table("Categories")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(500).NotNullable()
            .WithColumn("ParentId").AsInt32().Nullable();
    }

    public override void Down()
    {
        Delete.Table("Categories");
    }
}