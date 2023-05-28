using System.Text;
using FootballNeighborhood.Domain.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FootballNeighborhood.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var tokenKey = configuration.GetSection(TokenOptions.Key)
            .GetValue<string>("Secrete");

        var key = Encoding.ASCII.GetBytes(tokenKey ??
                                          throw new ArgumentException(
                                              "In appsettings secret for token was not provided"));

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            options.SaveToken = true;
            options.RequireHttpsMetadata = true;
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        context.Response.Headers.Add("Token-Expired", "true");
                    return Task.CompletedTask;
                },
                OnMessageReceived = context =>
                {
                    var token = context.Request.Query["access_token"];

                    if (!string.IsNullOrWhiteSpace(token)
                        && context.Request.Path.StartsWithSegments("/hubs"))
                        context.Token = token;

                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}