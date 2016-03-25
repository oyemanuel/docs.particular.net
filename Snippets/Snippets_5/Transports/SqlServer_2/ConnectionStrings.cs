﻿namespace Snippets5.Transports.SqlServer
{
    using System.Data.SqlClient;
    using NServiceBus;

    class NamedConnectionString
    {
        void ConnectionString(BusConfiguration busConfiguration)
        {
            #region sqlserver-config-connectionstring

            busConfiguration.UseTransport<SqlServerTransport>()
                .ConnectionString("Data Source=INSTANCE_NAME;Initial Catalog=some_database;Integrated Security=True");

            #endregion
        }

        void ConnectionName(BusConfiguration busConfiguration)
        {
            #region sqlserver-named-connection-string

            busConfiguration.UseTransport<SqlServerTransport>()
                .ConnectionStringName("MyConnectionString");

            #endregion
        }

        void ConnectionFactory(BusConfiguration busConfiguration)
        {
            #region sqlserver-custom-connection-factory

            busConfiguration.UseTransport<SqlServerTransport>()
                .UseCustomSqlConnectionFactory(
                    connectionString => new SqlConnection(@"Server=localhost\sqlexpress;Database=nservicebus;Trusted_Connection=True;"));

            #endregion
        }
    }
}