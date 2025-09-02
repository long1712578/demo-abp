using KafkaFlow;
using KafkaFlow.Middlewares.Serializer.Resolvers;
using Serilog;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TodoApp.Entities;

namespace TodoApp.TypeResolvers
{
    public class DragonTypeResolvers : IMessageTypeResolver
    {
        public async ValueTask<Type> OnConsumeAsync(IMessageContext context)
        {
            Log.Warning(JsonSerializer.Serialize(context.ConsumerContext));
            var value = context.Headers.SingleOrDefault(x => x.Key == "key").Value;
            var eto = Encoding.Default.GetString(value);
            var type = eto switch
            {
                "key123" => typeof(KafkaMessage),
                _ => null!
            };
            return await Task.FromResult(type);
        }

        public ValueTask OnProduceAsync(IMessageContext context)
        {
            throw new NotImplementedException();
        }
    }
}
