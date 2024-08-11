using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Dtos.MatchPlayerReview;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.MatchPlayerReviews.Queries;

public class GetMatchPlayerReviewsQueryHandler : IQueryHandler<GetMatchPlayerReviewsQuery, GetMatchPlayerReviewsQueryResult>
{
    private readonly Context _context;
    private readonly IUserContext _userContext;
    public GetMatchPlayerReviewsQueryHandler(Context context,
        IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<GetMatchPlayerReviewsQueryResult>> Handle(GetMatchPlayerReviewsQuery request, CancellationToken cancellationToken)
    {
        var matchPlayer = await _context.MatchPlayers
                 .Include(matchPlayer => matchPlayer.MatchPlayerReviews)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(matchPlayer => matchPlayer.MatchId == request.MatchId
                     && matchPlayer.UserId == _userContext.CurrentUserId, cancellationToken);

        var result = new OperationResult<GetMatchPlayerReviewsQueryResult>()
        {
            Result = new GetMatchPlayerReviewsQueryResult()
            {
                MatchReviewScore = matchPlayer!.MatchReviewScore,
                MatchOwnerReviewScore = matchPlayer!.MatchOwnerReviewScore,
                MatchReviewDescription = matchPlayer.MatchReviewDescription,
                PlayerReviews = matchPlayer.MatchPlayerReviews.Select(review =>
                    new GetMatchPlayerReviewInfoDto()
                    {
                        MatchPlayerId = review.ReviewedPlayerId,
                        PlayerScore = review.Score,
                        PlayerScoreDescription = review.Description
                    }
                )
            }
        };
    }
}

