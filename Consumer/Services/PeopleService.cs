using Domain.Interfaces;
using Domain.Models;
using System.Text.Json;

namespace Services
{
    public class PeopleService : IPeopleService
    {
        public void DoSomethingWithPeopleAsync(People p)
        {
            var parsed = JsonSerializer.Serialize(p);
            Console.WriteLine(parsed);
        }
    }
}
