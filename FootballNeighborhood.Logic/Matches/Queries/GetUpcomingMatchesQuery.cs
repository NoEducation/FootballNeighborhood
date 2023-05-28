using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Matches.Queries;

public record GetUpcomingMatchesQuery : IQuery<GetUpcomingMatchesQueryResult>
{
    public GetUpcomingMatchesQuery(int? userId)
    {
        UserId = userId;
    }

    public int? UserId { get; }
}