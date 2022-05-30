using Bogus;
using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Workers;
using Domain.Models;

namespace Services.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly List<People> _people;
        private readonly IWorker _worker;

        public PeopleService(IWorker worker)
        {
            _people = new Faker<People>()
                .RuleFor(p => p.Id, () => Guid.NewGuid())
                .RuleFor(p => p.Name, r => r.Person.FullName)
                .RuleFor(p => p.Phone, r => r.Person.Phone)
                .RuleFor(p => p.Email, r => r.Person.Email)
                .Generate(10);

            _worker = worker;
        }

        public async Task<bool> InsertListAsync() 
            => await Task.Run(async () =>
            {
                // Some pseudo code
                await _worker.SendListAsync(_people, QueueName.PEOPLE);

                // Another treatement
                await _worker.SendAsync(_people, QueueName.EMAIL); 
                return true;
            });

        public async Task<bool> InsertAsync(People people)
            => await Task.Run(async () =>
            {
                // Ficticious Repository Call
                _people.Add(people);
                await _worker.SendAsync(people, QueueName.PEOPLE);

                // Some logic
                // ...

                await _worker.SendAsync(people, QueueName.EMAIL);
                return true;
            });
    }
}
