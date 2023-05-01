using FootballNeighborhood.Services.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootballNeighborhood.Infrastructure.Dependencies;

public static class ContextModule
{
    public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(x => new Context(configuration.GetConnectionString("DefaultConnection") ??
                                            throw new ArgumentException("Connection string not provided !")));

        return services;
    }
}