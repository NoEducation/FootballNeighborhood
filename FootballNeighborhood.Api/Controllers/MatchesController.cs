using FootballNeighborhood.Domain.Consts.Permissions;
using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Infrastructure.Filters;
using FootballNeighborhood.Logic.Matches.Commands;
using FootballNeighborhood.Logic.Matches.Queries;
using FootballNeighborhood.Services.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers;

public class MatchesController : BaseController
{
    private readonly IUserContext _userContext;

    public MatchesController(IDispatcher dispatcher, IUserContext userContext) : base(dispatcher)
    {
        _userContext = userContext;
    }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = new object[] { Permissions.ViewMatches })]
    [HttpGet("GetAllMatches")]
    public async Task<OperationResult<GetAllMatchesQueryResult>> GetAllMatches(CancellationToken cancellationToken)
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
    public async Task<OperationResult<SuccessMessageAndObjectId>> CreateMatch([FromBody] CreateMatchCommand command,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }
}