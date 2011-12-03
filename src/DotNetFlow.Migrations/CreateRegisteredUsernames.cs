using System.Data;
using Migrator.Framework;

namespace DotNetFlow.Migrations
{
    /// <summary>
    /// Table for recording unique, registered usernames (ensure unique logins & prevent duplicate user registrations)
    /// </summary>
    [Migration(20110707075300)]
    public sealed class CreateRegisteredUsernames : Migration
    {
        public override void Up()
        {
            Database.AddTable("RegisteredUsernames",
                              new Column("UserId", DbType.Guid, ColumnProperty.PrimaryKey),
                              new Column("Username", DbType.String, 20)
                );

            Database.AddUniqueConstraint("IX_RegisteredUsernames_Unique_Username", "RegisteredUsernames", "Username");
        }

        public override void Down()
        {
            Database.RemoveTable("RegisteredUsernames");
        }        
    }
}