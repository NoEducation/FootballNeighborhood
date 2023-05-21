using FootballNeighborhood.Domain.Dtos.Matches;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class GetAvailableMatchesByCityQueryResult
{
    public IEnumerable<MatchDto> Matches { get; set; } = default!;
}