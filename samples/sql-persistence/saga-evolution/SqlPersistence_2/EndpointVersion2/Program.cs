using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Persistence.Sql;

class Program
{
    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        var defaultFactory = LogManager.Use<DefaultFactory>();
        defaultFactory.Level(LogLevel.Warn);

        Console.Title = "Samples.EvolveSaga.Version2";

        var endpointConfiguration = new EndpointConfiguration("Samples.EvolveSaga");
        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.SqlVariant(SqlVariant.MsSqlServer);
        var connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=SqlPersistenceSample;Integrated Security=True";
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(connection);
            });

        persistence.SagaSettings()
            .JsonSettingsForVersion(
                builder: (type, version) =>
                {
                    if (version == new Version(1, 0))
                    {
                        return null;
                    }
                    return new JsonSerializerSettings
                    {
                        ContractResolver = new ExtendedResolver()
                    };
                });

        SharedConfiguration.Apply(endpointConfiguration);

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

        Console.WriteLine("Waiting to receive timeout. Should happen within 10 seconds");
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}