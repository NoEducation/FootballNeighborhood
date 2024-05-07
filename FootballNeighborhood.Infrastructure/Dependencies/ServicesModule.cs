using FootballNeighborhood.Services.Authentications;
using FootballNeighborhood.Services.Emails;
using FootballNeighborhood.Services.Repositories;
using FootballNeighborhood.Services.UserContext;
using Microsoft.Extensions.DependencyInjection;

namespace FootballNeighborhood.Infrastructure.Dependencies;

public static class ServicesModule
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        AddDomainServices(services);
        AddRepositories(services);

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserConfirmationRepository, UserConfirmationRepository>();
    }

    private static void AddDomainServices(IServiceCollection services)
    {
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
    }
}