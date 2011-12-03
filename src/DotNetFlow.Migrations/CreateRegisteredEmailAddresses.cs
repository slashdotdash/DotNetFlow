using System.Data;
using Migrator.Framework;

namespace DotNetFlow.Migrations
{
    /// <summary>
    /// Table for recording unique, registered email addresses (prevent duplicate email registrations)
    /// </summary>
    [Migration(20110530140000)]
    public sealed class CreateRegisteredEmailAddresses : Migration
    {
        public override void Up()
        {
            Database.AddTable("RegisteredEmailAddresses",
                              new Column("UserId", DbType.Guid, ColumnProperty.PrimaryKey),
                              new Column("Email", DbType.String, 1000)
                );

            Database.AddUniqueConstraint("IX_RegisteredEmailAddresses_Unique_Email", "RegisteredEmailAddresses", "Email");
        }

        public override void Down()
        {
            Database.RemoveTable("RegisteredEmailAddresses");
        }        
    }
}