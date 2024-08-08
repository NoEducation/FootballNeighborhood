using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Users.Queries;

public class GetCurrentUserQuery : IQuery<GetCurrentUserQueryResult>
{
    public GetCurrentUserQuery(int userId)
    {
        UserId = userId;
    }

    public int UserId { get; }
}

