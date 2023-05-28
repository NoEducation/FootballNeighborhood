using FootballNeighborhood.Domain.Dtos.Matches;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class GetMatchByIdQueryResult
{
    public MatchDto Match { get; set; } = default!;
}