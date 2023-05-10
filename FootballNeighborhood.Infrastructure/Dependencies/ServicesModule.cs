using FootballNeighborhood.Services.Authentications;
using Microsoft.Extensions.DependencyInjection;

namespace FootballNeighborhood.Infrastructure.Dependencies;

public static class ServicesModule
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ILoginService, LoginService>();


        return services;
    }
}