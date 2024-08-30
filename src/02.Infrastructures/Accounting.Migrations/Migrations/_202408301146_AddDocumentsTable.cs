using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Accounting.Migrations.Migrations
{
    [Migration(202408301146)]
    public class _202408301146_AddDocumentsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Documents")
                .WithColumn("Id").AsCustom("VARCHAR(100)").NotNullable().PrimaryKey()
                .WithColumn("Data").AsBinary(int.MaxValue)
                .WithColumn("Extension").AsString(10).NotNullable()
                .WithColumn("Status").AsInt16().NotNullable()
                .WithColumn("CreationDate").AsDateTime2().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Documents");
        }
    }
}
