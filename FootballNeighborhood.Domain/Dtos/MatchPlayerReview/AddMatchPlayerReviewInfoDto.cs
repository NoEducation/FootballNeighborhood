namespace FootballNeighborhood.Domain.Dtos.MatchPlayerReview;

public class AddMatchPlayerReviewInfoDto
{
    public int UserId { get; set; }
    public short PlayerScore { get; set; }
    public string? PlayerScoreDescription { get; set; }
}

