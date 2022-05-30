using Domain.Interfaces;
using Domain.Interfaces.Services.Workers;

namespace Consumer
{
    public class Worker : BackgroundService
    {
        private readonly IWorker _worker;
        private readonly IPeopleService _peopleService;

        public Worker(IWorker worker, IPeopleService peopleService)
        {
            _worker = worker;
            _peopleService = peopleService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) => await _worker.ReceivePeople(_peopleService.DoSomethingWithPeopleAsync);
    }
}
