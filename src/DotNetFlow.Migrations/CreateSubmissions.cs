using System.Data;
using Migrator.Framework;

namespace DotNetFlow.Migrations
{
    [Migration(20110524213700)]
    public sealed class CreateSubmissions : Migration
    {
        public override void Up()
        {
            Database.AddTable("Submissions",
                              new Column("ItemId", DbType.Guid, ColumnProperty.PrimaryKey),
                              new Column("SubmittedAt", DbType.DateTime),
                              new Column("UserId", DbType.Guid, ColumnProperty.Null),
                              new Column("Username", DbType.String, 20),
                              new Column("FullName", DbType.String, 200),
                              new Column("Title", DbType.String, 140),
                              new Column("HtmlContent", DbType.String, 2000)
                );

            Database.AddIndex("Submissions", "SubmittedAt");
        }

        public override void Down()
        {
            Database.RemoveTable("Submissions");
        }
    }
}