namespace FootballNeighborhood.Domain.Dtos.Users;

public class CurrentUserMatchEvaluationDto
{
    public short Score { get; set; }
    public string UserName { get; set; } = default!;
    public DateTime Timestamp { get; set; } = default!;
}

