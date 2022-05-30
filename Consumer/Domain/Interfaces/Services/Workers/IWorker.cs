using Domain.Models;

namespace Domain.Interfaces.Services.Workers
{
    public interface IWorker
    {
        Task ReceivePeople(Action<People> ProcessItem);
    }
}
