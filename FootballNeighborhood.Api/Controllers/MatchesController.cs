using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Logic.Matches.Commands;
using FootballNeighborhood.Logic.Matches.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers;

public class MatchesController : BaseController
{
    public MatchesController(IDispatcher dispatcher) : base(dispatcher)
    {
    }


    [HttpGet("GetAllMatches")]
    public async Task<OperationResult<GetAllMatchesQueryResult>> GetAllMatches(CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetAllMatchesQuery(), cancellationToken);
    }

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