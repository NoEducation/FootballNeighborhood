using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    protected BaseController(IDispatcher dispatcher)
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

    protected async Task<OperationResult<SuccessMessage>> DispatchAsync<TResult>(ICommand<SuccessMessage> command,
        CancellationToken cancellationToken = default)
    {
        var result = await _dispatcher.SendAsync(command, cancellationToken);

        if (!result.Success) Response.StatusCode = StatusCodes.Status500InternalServerError;

        return result;
    }

    protected async Task<OperationResult<SuccessMessageAndObjectId>> DispatchAsync(
        ICommand<SuccessMessageAndObjectId> command,
        CancellationToken cancellationToken = default)
    {
        var result = await _dispatcher.SendAsync(command, cancellationToken);

        if (!result.Success) Response.StatusCode = StatusCodes.Status500InternalServerError;

        return result;
    }
}