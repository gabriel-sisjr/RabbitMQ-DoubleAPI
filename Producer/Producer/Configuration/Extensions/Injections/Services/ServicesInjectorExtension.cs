using Domain.Interfaces.Services;
using Services.Services;

namespace Producer.Configuration.Extensions.Injections.Services
{
    public static class ServicesInjectorExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection collection)
        {
            collection.AddScoped<IPeopleService, PeopleService>();

            return collection;
        }
    }
}
