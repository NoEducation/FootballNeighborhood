using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public BaseController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    protected async Task<OperationResult<TResult>> DispatchAsync<TResult>(IQuery<TResult> query,
        CancellationToken cancellationToken = default)
    {
        var result = await _dispatcher.SendAsync(query, cancellationToken);

        if (!result.Success) Response.StatusCode = StatusCodes.Status500InternalServerError;

        return result;
    }

    protected async Task<OperationResult> DispatchAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        var result = await _dispatcher.SendAsync(command, cancellationToken);

        if (!result.Success) Response.StatusCode = StatusCodes.Status500InternalServerError;

        return result;
    }
}