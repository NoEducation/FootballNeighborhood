using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Matches.Queries;

public record GetAvailableMatchesByCityQuery : IQuery<GetAvailableMatchesByCityQueryResult>
{
    public GetAvailableMatchesByCityQuery(string city)
    {
        City = city;
    }

    public string City { get; }
}