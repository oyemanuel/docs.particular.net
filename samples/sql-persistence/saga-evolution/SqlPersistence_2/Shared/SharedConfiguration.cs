using NServiceBus;

public static class SharedConfiguration
{

    public static void Apply(EndpointConfiguration endpointConfiguration)
    {
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        endpointConfiguration.EnableInstallers();
    }
}