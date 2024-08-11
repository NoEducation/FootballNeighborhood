using FootballNeighborhood.Domain.Dtos.MatchPlayerReview;

namespace FootballNeighborhood.Logic.MatchPlayerReviews.Queries;

public class GetMatchPlayerReviewsQueryResult
{
    public short? MatchReviewScore { get; set; }
    public short? MatchOwnerReviewScore { get; set; }
    public string? MatchReviewDescription { get; set; }
    public IEnumerable<GetMatchPlayerReviewInfoDto>? PlayerReviews { get; set; }
}

