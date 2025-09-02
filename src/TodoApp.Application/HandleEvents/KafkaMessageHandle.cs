using KafkaFlow;
using System;
using System.Threading.Tasks;
using TodoApp.Entities;

namespace TodoApp.HandleEvents
{
    public class KafkaMessageHandle : IMessageHandler<KafkaMessage>
    {
        public Task Handle(IMessageContext context, KafkaMessage message)
        {
            /// TODO: Handle the message
            /// 
            Console.WriteLine($"Received message: {message.Content}");
            return Task.CompletedTask;
        }
    }
}
