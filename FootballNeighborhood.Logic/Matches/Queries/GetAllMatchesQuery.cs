using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Matches.Queries;

public record GetAllMatchesQuery : IQuery<GetAllMatchesQueryResult>
{
}