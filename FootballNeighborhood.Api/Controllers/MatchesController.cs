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
    [HttpGet("GetAllMatches")]
    public async Task<OperationResult<GetAllMatchesQueryResult>> GetAllMatches(CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetAllMatchesQuery(), cancellationToken);
    }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.ViewMatches })]
    [HttpGet("GetAssignedUpcommigMatches")]
    public async Task<OperationResult<GetAllMatchesQueryResult>> GetUpcommigMatches(CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetAllMatchesQuery(), cancellationToken);
    }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.ViewMatches })]
    [HttpGet("GetAvailableMatchesByCity")]
    public async Task<OperationResult<GetAvailableMatchesByCityQueryResult>> GetAvailableMatchesByCity(
        [FromQuery] string city,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetAvailableMatchesByCityQuery(city), cancellationToken);
    }

    [HttpPost("CreateMatch")]
    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.SaveMatch })]
    public async Task<OperationResult<SuccessMessageAndObjectId>> CreateMatch([FromBody] CreateMatchCommand command,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }

    [HttpPost("EditMatch")]
    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.SaveMatch })]
    public async Task<OperationResult<SuccessMessage>> EditMatch([FromBody] UpdateMatchCommand command,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }
}