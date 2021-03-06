﻿using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace DotNetFlow.Specifications.Infrastructure
{
    internal sealed class EventStoreCleaner
    {
        public void Execute()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EventStore"].ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = DeleteCommandText();
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }

        private static string DeleteCommandText()
        {
            return string.Concat(EventStoreTables.Select(table => string.Format("delete from {0};", table)));
        }

        private static IEnumerable<string> EventStoreTables
        {
            get { return new[] { "Commits", "Snapshots" }; }
        }
    }
}
