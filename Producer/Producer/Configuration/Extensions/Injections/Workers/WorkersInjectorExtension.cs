using Domain.Models.Workers;
using Services.Workers;

namespace Producer.Configuration.Extensions.Injections.Workers
{
    public static class WorkersInjectorExtension
    {
        public static IServiceCollection AddWorkersSetup(this IServiceCollection collection, IConfiguration configuration)
        {
            if (Environment.GetEnvironmentVariable("AMBIENT") == "CONTAINER")
            {
                var settings = configuration.GetSection("RabbitMqSettings").Get<RabbitSettings>();
                collection.AddSingleton(x => RabbitSetup.CreateBus(settings!));
            }
            else
                collection.AddSingleton(x => RabbitSetup.CreateBus("localhost"));

            return collection;
        }
    }
}
