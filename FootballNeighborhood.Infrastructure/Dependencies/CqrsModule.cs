using System.Reflection;
using FootballNeighborhood.Infrastructure.Cqrs;
using Microsoft.Extensions.DependencyInjection;

namespace FootballNeighborhood.Infrastructure.Dependencies;

public static class CqrsModule
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(Assembly.Load("FootballNeighborhood.Logic")));
        services.AddScoped<IDispatcher, Dispatcher>();

        return services;
    }
}