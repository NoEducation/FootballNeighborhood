using FootballNeighborhood.Domain.Dtos.Matches;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class GetMatchByIdQueryResult
{
    public MatchDetailsDto Match { get; set; } = default!;
}