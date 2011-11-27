using System.Data;
using Migrator.Framework;

namespace DotNetFlow.Migrations
{
    [Migration(20110530133800)]
    public sealed class CreateUsers : Migration
    {
        public override void Up()
        {
            Database.AddTable("Users",
                              new Column("UserId", DbType.Guid, ColumnProperty.PrimaryKey),
                              new Column("RegisteredAt", DbType.DateTime),
                              new Column("FullName", DbType.String, 200),
                              new Column("Username", DbType.String, 20),
                              new Column("Email", DbType.String, 1000),
                              new Column("HashedPassword", DbType.StringFixedLength, 60),
                              new Column("Website", DbType.String, 1000),
                              new Column("Twitter", DbType.String, 20)
                );

            Database.AddIndex("Users", "Username", "Email");  // Used to authenticate users during login
            Database.AddIndex("Users", "Username");
            Database.AddIndex("Users", "Email");

            Database.AddUniqueConstraint("IX_Users_Unique_Username", "Users", "Username");
            Database.AddUniqueConstraint("IX_Users_Unique_Email", "Users", "Email");
        }

        public override void Down()
        {
            Database.RemoveTable("Users");
        }
    }
}