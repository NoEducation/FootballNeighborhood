using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Matches.Queries;

public record GetMatchByIdQuery : IQuery<GetMatchByIdQueryResult>
{
    public GetMatchByIdQuery(int matchId)
    {
        MatchId = matchId;
    }

    public int MatchId { get; }
}