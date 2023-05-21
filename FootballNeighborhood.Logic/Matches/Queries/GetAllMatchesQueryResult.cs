using FootballNeighborhood.Domain.Dtos.Matches;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class GetAllMatchesQueryResult
{
    public IEnumerable<MatchDto> Matches { get; set; } = default!;
}