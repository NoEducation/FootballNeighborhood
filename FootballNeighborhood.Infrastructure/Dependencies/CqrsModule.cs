using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace FootballNeighborhood.Infrastructure.Dependencies;

public static class CqrsModule
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(typeof(EmptyClass).Assembly));
        services.AddScoped<IDispatcher, Dispatcher>();

        return services;
    }
}