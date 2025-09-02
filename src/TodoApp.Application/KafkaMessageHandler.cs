using KafkaFlow;
using System;
using System.Threading.Tasks;
using TodoApp.Entities;

namespace TodoApp
{
    public class KafkaMessageHandler : IMessageHandler<KafkaMessage>
    {
        public Task Handle(IMessageContext context, KafkaMessage message)
        {
            Console.WriteLine($"Received message: {message.Content}");
            return Task.CompletedTask;
        }
    }
}
