using System.IO;
using System;
using Simpler;
using System.Data;
using System.Configuration;
using System.Data.Common;

namespace Chic
{
    public static class TaskExtensions
    {
        public static IDbConnection Connect(this Task task, string connectionName)
        {
            var connectionConfig = ConfigurationManager.ConnectionStrings[connectionName];
            if (connectionConfig == null) throw new Exception(String.Format(
                "A connectionString with name {0} was not found in the configuration file.", 
                connectionName
            ));

            var connectionString = connectionConfig.ConnectionString;
            var providerName = connectionConfig.ProviderName;
            var provider = DbProviderFactories.GetFactory(providerName);

            var connection = provider.CreateConnection();
            if (connection == null) throw new Exception(String.Format(
                @"Error creating DbProviderFactory connection using a connectionString {0} with a provider type of {1}.",
                connectionName,
                providerName
            ));

            connection.ConnectionString = connectionString;
            connection.Open();

            return connection;
        }

        public static string Sql(this Task task)
        {
            var resourceName = String.Concat(task.Name.Replace(".Tasks.", ".Sql."), ".sql");
            var assembly = task.GetType().BaseType.Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}