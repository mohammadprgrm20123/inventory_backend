using FluentMigrator;

namespace Accounting.Migrations.Migrations;

[Migration(202411041952)]
public class _202411041952_CreateUnitTable : Migration
{
    public override void Up()
    {
        Create.Table("Units")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(500).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Units");
    }
}