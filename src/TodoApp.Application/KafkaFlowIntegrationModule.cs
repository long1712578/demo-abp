using KafkaFlow;
using KafkaFlow.Serializer;
using Volo.Abp.Modularity;

namespace TodoApp
{
    [DependsOn(typeof(TodoAppApplicationModule))]
    public class KafkaFlowIntegrationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddKafka(kafka =>
            {
                kafka.UseConsoleLog()
                    .AddCluster(cluster =>
                    {
                        cluster.WithBrokers(new[] { "localhost:9092" })
                            .AddConsumer(consumer =>
                            {
                                consumer.Topic("demo")
                                    .WithGroupId("demo-group")
                                    .WithBufferSize(100)
                                    .WithWorkersCount(4)
                                    .AddMiddlewares(middlewares =>
                                    {
                                        // Use AddDeserializer instead of AddSerializer for consumers
                                        middlewares.AddDeserializer<JsonCoreDeserializer>();
                                        middlewares.AddTypedHandlers(handlers =>
                                            handlers.AddHandler<KafkaMessageHandler>());
                                    });
                            });
                    });
            });
        }
    }
}