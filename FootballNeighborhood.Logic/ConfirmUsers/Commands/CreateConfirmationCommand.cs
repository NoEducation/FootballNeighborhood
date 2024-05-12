using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.ConfirmUsers.Commands;

public class CreateConfirmationCommand : ICommand<SuccessMessage>
{
    public int UserId { get; set; }
}

