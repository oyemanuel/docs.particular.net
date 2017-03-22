using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Persistence.Sql;

public class MySaga :
    SqlSaga<MySaga.SagaData>,
    IAmStartedByMessages<StartSaga>,
    IHandleTimeouts<SagaTimeout>
{
    static ILog log = LogManager.GetLogger<MySaga>();

    protected override void ConfigureMapping(IMessagePropertyMapper mapper)
    {
        mapper.ConfigureMapping<StartSaga>(_ => _.TheId);
    }

    protected override string CorrelationPropertyName => nameof(SagaData.TheId);

    public Task Handle(StartSaga message, IMessageHandlerContext context)
    {
        // throw only for sample purposes
        throw new Exception("Expected StartSaga in MyTimeoutSagaVersion1.");
    }

    public Task Timeout(SagaTimeout state, IMessageHandlerContext context)
    {
        log.Warn("Received Timeout");
        MarkAsComplete();
        return Task.FromResult(0);
    }

    public class SagaData :
        ContainSagaData
    {
        public Guid TheId { get; set; }
        public Dictionary<int, string> DictionaryProperty { get; set; }
    }
}