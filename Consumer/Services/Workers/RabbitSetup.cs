using Domain.Interfaces.Services.Workers;
using Domain.Models.Workers;
using RabbitMQ.Client;

namespace Services.Workers
{
    public class RabbitSetup
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _channel;

        public static IWorker CreateBus(string hostName)
        {
            _factory = new ConnectionFactory { HostName = hostName, DispatchConsumersAsync = true };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            return new ServiceWorker(_channel);
        }

        public static IWorker CreateBus(RabbitSettings settings)
        {
            _factory = new ConnectionFactory
            {
                HostName = settings.HostName,
                Port = settings.Port,
                VirtualHost = settings.VirtualHost,
                UserName = settings.Username,
                Password = settings.Password,
                DispatchConsumersAsync = true
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            return new ServiceWorker(_channel);
        }
    }
}
