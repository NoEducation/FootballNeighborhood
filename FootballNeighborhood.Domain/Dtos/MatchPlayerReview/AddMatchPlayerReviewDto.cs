namespace FootballNeighborhood.Domain.Dtos.MatchPlayerReview;

public class AddMatchPlayerReviewDto
{
    public int PlayerId { get; set; }
    public short PlayerScore { get; set; }
    public string? PlayerScoreDescription { get; set; }
}
