using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Enums.Users;
using FootballNeighborhood.Infrastructure.Cqrs;


namespace FootballNeighborhood.Logic.Users.Commands;

public class UpdateCurrentUserCommand : ICommand<SuccessMessage>
{
    public int UserId { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public GenderEnum? Gender { get; set; }
    public string Description { get; set; } = default!;
}

