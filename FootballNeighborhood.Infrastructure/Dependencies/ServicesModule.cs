using FootballNeighborhood.Services.Authentications;
using FootballNeighborhood.Services.Emails;
using FootballNeighborhood.Services.UserContext;
using Microsoft.Extensions.DependencyInjection;

namespace FootballNeighborhood.Infrastructure.Dependencies;

public static class ServicesModule
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}