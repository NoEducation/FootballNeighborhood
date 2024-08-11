

using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Matches.Commands;

public class FinishMatchCommand : ICommand<SuccessMessage>
{
    public int MatchId { get; }

    public FinishMatchCommand(int matchId)
    {
        MatchId = matchId;
    }
}

