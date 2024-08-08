namespace FootballNeighborhood.Domain.Dtos.Users;

public class CurrentUserMatchDto
{
    public string MatchName { get; set; } = default!;
    public string City { get; set; } = default!;
    public IEnumerable<CurrentUserMatchEvaluationDto> MatchesEvaluations { get; set; } = default!;
    public short AverageScore { get; set; } 
}

