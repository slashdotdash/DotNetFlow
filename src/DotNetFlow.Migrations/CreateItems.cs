using System.Data;
using Migrator.Framework;

namespace DotNetFlow.Migrations
{
    [Migration(20110620213300)]
    public sealed class CreateItems : Migration
    {
        public override void Up()
        {
            Database.AddTable("Items",
                              new Column("ItemId", DbType.Guid, ColumnProperty.PrimaryKey),
                              new Column("PublishedAt", DbType.DateTime),
                              new Column("SubmittedByUser", DbType.String, 1000),
                              new Column("Title", DbType.String, 140),
                              new Column("HtmlContent", DbType.String, 2000)
                );
        }

        public override void Down()
        {
            Database.RemoveTable("Items");
        }
    }
}