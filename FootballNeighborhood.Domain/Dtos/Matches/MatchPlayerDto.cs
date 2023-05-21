using FootballNeighborhood.Domain.Enums.Match;

namespace FootballNeighborhood.Domain.Dtos.Matches;

public class MatchPlayerDto
{
    public int UserId { get; set; }
    public int MatchId { get; set; }
    public PlayerType PlayerType { get; set; }
    public string UserDisplayName { get; set; } = default!;
}