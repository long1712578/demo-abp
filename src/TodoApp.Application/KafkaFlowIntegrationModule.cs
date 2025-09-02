using KafkaFlow;
using KafkaFlow.Serializer;
using TodoApp.TypeResolvers;
using Volo.Abp.Modularity;

namespace TodoApp
{
    //[DependsOn(typeof(TodoAppApplicationModule))]
    public class KafkaFlowIntegrationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddKafka(kafka =>
            {
                kafka.UseConsoleLog()
                    .AddCluster(cluster =>
                    {
                        cluster.WithBrokers(["localhost:9092"])
                            .AddConsumer(consumer =>
                            {
                                consumer.Topic("demo-topic")
                                    .WithGroupId("demo-group-new-1")
                                    .WithBufferSize(100)
                                    .WithWorkersCount(4)
                                    .AddMiddlewares(middlewares =>
                                    {
                                        middlewares.AddDeserializer<JsonCoreDeserializer>(); // Sử dụng deserializer cho consumer
                                        middlewares.AddTypedHandlers(handlers => handlers.AddHandler<KafkaMessageHandler>());
                                        middlewares.Add<ErrorLoggingMiddleware>(); // Thêm middleware log lỗi
                                    });
                            });
                    });
            });

        }
    }
}