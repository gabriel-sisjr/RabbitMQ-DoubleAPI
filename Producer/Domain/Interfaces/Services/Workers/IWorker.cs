using Domain.Enums;

namespace Domain.Interfaces.Services.Workers
{
    public interface IWorker
    {
        Task SendAsync<T>(T content, QueueName queueName);
        Task SendListAsync<T>(List<T> list, QueueName queueName);
    }
}
