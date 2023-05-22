using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Matches.Commands;

public class RemoveMatchCommand : ICommand<SuccessMessage>
{
    public int MatchId { get; set; }
}