using System.Net;
using FootballNeighborhood.Domain.Dtos.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace FootballNeighborhood.Infrastructure.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        IWebHostEnvironment hostingEnvironment)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, hostingEnvironment);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context,
        Exception exception,
        IWebHostEnvironment hostingEnvironment)
    {
        var code = Guid.NewGuid();

        var result = new OperationResultWithGenericError(code);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        if (hostingEnvironment.IsDevelopment())
        {
            result.AddError(exception.Message);
        }
        else
        {
            result.DisplayGenericException = true;
            result.AddError("Internal server error");
        }

        await context.Response.WriteAsync(
            JsonConvert.SerializeObject(result));
    }
}