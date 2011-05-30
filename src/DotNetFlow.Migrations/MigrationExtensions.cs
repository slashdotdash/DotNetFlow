using Migrator.Framework;

namespace DotNetFlow.Migrations
{
    internal static class MigrationExtensions
    {
        public static void AddIndex(this ITransformationProvider database, string table, params string[] columns)
        {
            var name = string.Concat("IX_", table, "_", string.Join("_", columns));
            database.ExecuteNonQuery(string.Format("CREATE INDEX {0} ON {1} ({2}) ", name, table, string.Join(", ", columns)));
        }
    }
}