using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace DotNetFlow.Utils
{
    internal sealed class DatabaseCleaner
    {
        public void Execute()
        {
            Delete(ReadModelTables);
            Delete(EventStoreTables);
        }

        private static void Delete(IEnumerable<string> tables)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DotNetFlow"].ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = DeleteCommandText(tables);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }

        private static string DeleteCommandText(IEnumerable<string> tables)
        {
            return string.Concat(tables.Select(table => string.Format("delete from {0};", table)));
        }

        private static IEnumerable<string> EventStoreTables
        {
            get { return new[] { "Commits", "Snapshots" }; }
        }

        private static IEnumerable<string> ReadModelTables
        {
            get { return new[] { "Items", "RegisteredEmailAddresses", "RegisteredUsernames", "Submissions", "Users", "UrlSlugs" }; }
        }
    }
}