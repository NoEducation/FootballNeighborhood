using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.MatchPlayers.Commands;

public class AssignToMatchCommand : ICommand<SuccessMessage>
{
    public int MatchId { get; set; }
}