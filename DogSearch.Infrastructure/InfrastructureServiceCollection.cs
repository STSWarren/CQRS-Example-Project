using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DogSearch.Infrastructure;

public static class InfrastructureServiceCollection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IDogRepository, DogRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IShowRepository, ShowRepository>();
        services.AddScoped<IPlacementRepository, PlacementRepository>();
        return services;
    }
}