using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Dtos.MatchPlayerReview;
using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.MatchPlayerReviews.Commands;

public class AddMatchReviewCommand : ICommand<SuccessMessage>
{
    public int MatchId { get; set; }
    public short MatchReviewScore { get; set; }
    public string? MatchReviewDescription { get; set; }
    public IEnumerable<AddMatchPlayerReviewDto>? PlayerReviews { get; set; }
}

