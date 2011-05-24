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
                              new Column("UsersName", DbType.String, 1000),
                              new Column("Title", DbType.String, 140),
                              new Column("HtmlContent", DbType.String, 2000)
                );
        }

        public override void Down()
        {
            Database.RemoveTable("Submissions");
        }
    }
}