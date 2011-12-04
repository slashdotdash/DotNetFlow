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
                              new Column("SubmittedByUserId", DbType.Guid),
                              new Column("SubmittedByUsername", DbType.String, 20),
                              new Column("SubmittedByFullName", DbType.String, 200),
                              new Column("Title", DbType.String, 140),
                              new Column("HtmlContent", DbType.String, 2000),
                              new Column("UrlSlug", DbType.String, 1000)
                );

            Database.AddIndex("Items", "PublishedAt");
            Database.AddIndex("Items", "SubmittedByUserId", "PublishedAt");
            Database.AddIndex("Items", "Title");
            Database.AddIndex("Items", "UrlSlug");
        }

        public override void Down()
        {
            Database.RemoveTable("Items");
        }
    }
}