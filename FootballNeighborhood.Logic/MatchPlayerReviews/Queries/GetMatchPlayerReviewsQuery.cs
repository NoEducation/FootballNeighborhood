using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.MatchPlayerReviews.Queries;

public class GetMatchPlayerReviewsQuery : IQuery<GetMatchPlayerReviewsQueryResult>
{
    public int MatchId { get; set; }

    public GetMatchPlayerReviewsQuery(int matchId)
    {
        MatchId = matchId;
    }
}

