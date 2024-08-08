using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Matches.Queries;

public record GetUserAssingedMatchesQuery : IQuery<GetUserAssingedMatchesQueryResult>
{
    public GetUserAssingedMatchesQuery(int? userId)
    {
        UserId = userId;
    }

    public int? UserId { get; }
}