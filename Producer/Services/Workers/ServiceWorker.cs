using Domain.Enums;
using Domain.Interfaces.Services.Workers;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Services.Workers
{
    internal class ServiceWorker : IWorker
    {
        private readonly IModel _channel;
        internal ServiceWorker(IModel channel) => _channel = channel;

        public async Task SendAsync<T>(T content, QueueName queueName)
        {
            var queue = queueName.ToString();
            await Task.Run(() =>
            {
                _channel.QueueDeclare(queue, true, false, false);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = false;

                var output = JsonSerializer.Serialize(content);
                _channel.BasicPublish(string.Empty, queue, null, Encoding.UTF8.GetBytes(output));
            });
        }
    }
}
