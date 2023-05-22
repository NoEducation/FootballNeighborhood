using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.MatchPlayers.Commands;

public class WriteOutMatchPlayerFromMatchCommand : ICommand<SuccessMessage>
{
    public int MatchId { get; set; }
    public int UserId { get; set; }
}