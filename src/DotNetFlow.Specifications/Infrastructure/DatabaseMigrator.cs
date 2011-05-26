using System.Configuration;
using System.Reflection;
using Migrator.Providers.SqlServer;

namespace DotNetFlow.Specifications.Infrastructure
{
    internal sealed class DatabaseMigrator
    {
        public void MigrateToLastVersion()
        {
            GetMigrator().MigrateToLastVersion();
        }

        private static Migrator.Migrator GetMigrator()
        {
            var assembly = Assembly.LoadFrom("DotNetFlow.Migrations.dll");
            var provider = new SqlServerTransformationProvider(new SqlServerDialect(), ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString);

            return new Migrator.Migrator(provider, assembly, false);
        }
    }
}