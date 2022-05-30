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
                .Generate(10);

            _worker = worker;
        }

        public async Task<IEnumerable<People>> GetAllAsync() => await Task.Run(() => _people);

        public async Task<bool> InsertAsync(People people)
            => await Task.Run(async () =>
            {
                _people.Add(people);
                await _worker.SendAsync(people, QueueName.PERSON);
                return true;
            });
    }
}
