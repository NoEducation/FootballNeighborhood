using FootballNeighborhood.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace FootballNeighborhood.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}