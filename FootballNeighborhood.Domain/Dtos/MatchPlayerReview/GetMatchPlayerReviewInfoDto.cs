namespace FootballNeighborhood.Domain.Dtos.MatchPlayerReview;

public class GetMatchPlayerReviewInfoDto
{
    public int MatchPlayerId { get; set; }
    public short PlayerScore { get; set; }
    public string? PlayerScoreDescription { get; set; }
}
