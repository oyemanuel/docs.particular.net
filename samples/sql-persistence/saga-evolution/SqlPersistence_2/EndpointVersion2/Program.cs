using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

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
        SharedConfiguration.Apply(endpointConfiguration);

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

        Console.WriteLine("Waiting to receive timeout. Should happen withing 10 seconds");
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}