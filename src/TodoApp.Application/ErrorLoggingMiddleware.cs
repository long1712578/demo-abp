using KafkaFlow;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TodoApp
{
    public class ErrorLoggingMiddleware : IMessageMiddleware
    {
        private readonly ILogger<ErrorLoggingMiddleware> _logger;
        public ErrorLoggingMiddleware(ILogger<ErrorLoggingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kafka consumer error: {Message}", ex.Message);
                throw;
            }
        }
    }
}
