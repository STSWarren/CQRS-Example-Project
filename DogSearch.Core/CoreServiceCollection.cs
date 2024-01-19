using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DogSearch.Core;

public static class CoreServiceCollection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}