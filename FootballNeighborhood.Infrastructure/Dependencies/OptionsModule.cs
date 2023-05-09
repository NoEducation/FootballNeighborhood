using FootballNeighborhood.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootballNeighborhood.Infrastructure.Dependencies;

public static class OptionsModule
{
    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOptions>(options
            => configuration.GetSection(TokenOptions.Key).Bind(options));

        return services;
    }
}