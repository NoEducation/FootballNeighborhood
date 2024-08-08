using FootballNeighborhood.Domain.Consts.Permissions;
using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Infrastructure.Filters;
using FootballNeighborhood.Logic.Matches.Commands;
using FootballNeighborhood.Logic.Matches.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers;

public class MatchesController : BaseController
{
    public MatchesController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = [Permissions.ViewMatches])]
    [HttpGet("getAllMatches")]
    public async Task<OperationResult<GetAllMatchesQueryResult>> GetAllMatches(CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetAllMatchesQuery(), cancellationToken);
    }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = [Permissions.ViewMatches])]
    [HttpGet("getAvailableMatchesByCity")]
    public async Task<OperationResult<GetAvailableMatchesByCityQueryResult>> GetAvailableMatchesByCity(
        [FromQuery] string city,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetAvailableMatchesByCityQuery(city), cancellationToken);
    }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = [Permissions.ViewMatches])]
    [HttpGet("getUserAssingedMatches")]
    public async Task<OperationResult<GetUserAssingedMatchesQueryResult>> GetUserAssingedMatches(
        CancellationToken cancellationToken, [FromQuery] int? userId = null)
    {
        return await DispatchAsync(new GetUserAssingedMatchesQuery(userId), cancellationToken);
    }


    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = [Permissions.ViewMatches])]
    [HttpGet("getUserOrganizedMatches")]
    public async Task<OperationResult<GetUserOrganizedMatchesQueryResult>> GetUserOrganizedMatches(
        CancellationToken cancellationToken, [FromQuery] int? userId = null)
    {
        return await DispatchAsync(new GetUserOrganizedMatchesQuery(userId), cancellationToken);
    }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = [Permissions.ViewMatches])]
    [HttpGet("getMatchById")]
    public async Task<OperationResult<GetMatchByIdQueryResult>> GetMatchById([FromQuery] int matchId,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetMatchByIdQuery(matchId), cancellationToken);
    }

    [HttpPost("createMatch")]
    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = [Permissions.SaveMatch])]
    public async Task<OperationResult<SuccessMessageAndObjectId>> CreateMatch([FromBody] CreateMatchCommand command,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }

    [HttpPost("updateMatch")]
    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = [Permissions.SaveMatch])]
    public async Task<OperationResult<SuccessMessage>> EditMatch([FromBody] UpdateMatchCommand command,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }

    [HttpPost("removeMatch")]
    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = [Permissions.DeleteMatch])]
    public async Task<OperationResult<SuccessMessage>> RemoveMatch([FromBody] int matchId,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(new RemoveMatchCommand(matchId), cancellationToken);
    }
}