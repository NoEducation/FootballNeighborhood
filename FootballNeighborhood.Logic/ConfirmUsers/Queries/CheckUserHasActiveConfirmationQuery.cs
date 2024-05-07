using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.ConfirmUsers.Queries;

public class CheckUserHasActiveConfirmationQuery : IQuery<CheckUserHasActiveConfirmationQueryResult>
{
    public int UserId { get; set; }
}

