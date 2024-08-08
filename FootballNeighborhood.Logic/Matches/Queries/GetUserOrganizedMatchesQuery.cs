using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Matches.Queries;

public class GetUserOrganizedMatchesQuery : IQuery<GetUserOrganizedMatchesQueryResult>
{
    public GetUserOrganizedMatchesQuery(int? userId)
    {
        UserId = userId;
    }

    public int? UserId { get; }
}

