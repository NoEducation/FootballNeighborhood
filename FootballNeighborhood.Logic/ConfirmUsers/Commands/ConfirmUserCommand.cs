using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.ConfirmUsers.Commands;

public record ConfirmUserCommand : ICommand<SuccessMessage>
{
    public int UserId { get; set; }
    public string Code { get; set; } = default!;
}
