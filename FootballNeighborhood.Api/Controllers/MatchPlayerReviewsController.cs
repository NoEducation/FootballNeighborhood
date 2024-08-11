using FootballNeighborhood.Domain.Consts.Permissions;
using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Infrastructure.Filters;
using FootballNeighborhood.Logic.MatchPlayerReviews.Commands;
using FootballNeighborhood.Logic.MatchPlayerReviews.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers;

public class MatchPlayerReviewsController : BaseController
{
    public MatchPlayerReviewsController(IDispatcher dispatcher) : base(dispatcher)
    { }

    [TypeFilter(typeof(PermissionAuthorizationAttribute), Arguments = [Permissions.AddMatchPlayerReview])]
    [HttpPost("addMatchPlayerReview")]
    public async Task<OperationResult<SuccessMessage>> AssingToMatch([FromBody] AddMatchReviewCommand command, CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }

 
    [HttpPost("getMatchPlayerReviews")]
    public async Task<OperationResult<GetMatchPlayerReviewsQueryResult>> GetMatchPlayerReviews([FromQuery] int matchId, CancellationToken cancellationToken)
    {
        return await DispatchAsync(new GetMatchPlayerReviewsQuery(matchId), cancellationToken);
    }
}

