using Domain.Interfaces.Services.Workers;
using Domain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Services.Workers
{
    internal class ServiceWorker : IWorker
    {
        private readonly IModel _channel;

        internal ServiceWorker(IModel channel) => _channel = channel;

        public async Task ReceivePeople(Action<People> ProcessItem)
        {
            var queue = "PEOPLE";
            _channel.QueueDeclare(queue, true, false, false);
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (s, e) =>
            {
                var jsonSpecified = Encoding.UTF8.GetString(e.Body.Span);
                var item = JsonSerializer.Deserialize<People>(jsonSpecified);

                // Doing something with people, ficticious code.
                ProcessItem(item!);

                await Task.Yield();
            };
            _channel.BasicConsume(queue, true, consumer);
            await Task.Yield();
        }
    }
}
