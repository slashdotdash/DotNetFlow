using System.Data;
using Migrator.Framework;

namespace DotNetFlow.Migrations
{
    /// <summary>
    /// Table for recording unique, registered URL slugs (prevent duplicates)
    /// </summary>
    [Migration(20111203003000)]
    public sealed class CreateUrlSlugs : Migration
    {
        public override void Up()
        {
            Database.AddTable("UrlSlugs",
                              new Column("ItemId", DbType.Guid, ColumnProperty.PrimaryKey),
                              new Column("Slug", DbType.String, 1000)
                );

            Database.AddUniqueConstraint("IX_UrlSlugs_Unique_Slug", "UrlSlugs", "Slug");
        }

        public override void Down()
        {
            Database.RemoveTable("UrlSlugs");
        }        
    }
}