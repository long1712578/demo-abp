using System;

namespace TodoApp.Entities
{
    public class KafkaMessage
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; } = default!;
    }
}
