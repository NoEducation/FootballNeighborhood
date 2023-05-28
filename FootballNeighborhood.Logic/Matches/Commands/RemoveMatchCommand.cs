using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Matches.Commands;

public record RemoveMatchCommand : ICommand<SuccessMessage>
{
    public RemoveMatchCommand(int matchId)
    {
        MatchId = matchId;
    }

    public int MatchId { get; }
}