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

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.ViewMatches })]
    [HttpGet("getAllMatches")]
    public async Task<OperationResult<GetAllMatchesQueryResult>> GetAllMatches(CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetAllMatchesQuery(), cancellationToken);
    }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.ViewMatches })]
    [HttpGet("getAvailableMatchesByCity")]
    public async Task<OperationResult<GetAvailableMatchesByCityQueryResult>> GetAvailableMatchesByCity(
        [FromQuery] string city,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetAvailableMatchesByCityQuery(city), cancellationToken);
    }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.ViewMatches })]
    [HttpGet("getUpcomingMatches")]
    public async Task<OperationResult<GetUpcomingMatchesQueryResult>> GetUpcomingMatches(
        CancellationToken cancellationToken, [FromQuery] int? userId = null)
    {
        return await DispatchAsync(new GetUpcomingMatchesQuery(userId), cancellationToken);
    }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.ViewMatches })]
    [HttpGet("getMatchById")]
    public async Task<OperationResult<GetMatchByIdQueryResult>> GetMatchById([FromQuery] int matchId,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetMatchByIdQuery(matchId), cancellationToken);
    }

    [HttpPost("createMatch")]
    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.SaveMatch })]
    public async Task<OperationResult<SuccessMessageAndObjectId>> CreateMatch([FromBody] CreateMatchCommand command,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }

    [HttpPost("editMatch")]
    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.SaveMatch })]
    public async Task<OperationResult<SuccessMessage>> EditMatch([FromBody] UpdateMatchCommand command,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }

    [HttpPost("removeMatch")]
    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.DeleteMatch })]
    public async Task<OperationResult<SuccessMessage>> RemoveMatch([FromBody] int matchId,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(new RemoveMatchCommand(matchId), cancellationToken);
    }
}