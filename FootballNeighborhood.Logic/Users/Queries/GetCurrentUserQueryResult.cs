using FootballNeighborhood.Domain.Dtos.Users;
using FootballNeighborhood.Domain.Enums.Roles;
using FootballNeighborhood.Domain.Enums.Users;

namespace FootballNeighborhood.Logic.Users.Queries;

public class GetCurrentUserQueryResult
{
    public int UserId { get; set; }
    public string Surname { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public RolesEnum Role { get; set; }
    public string Phone { get; set; } = default!;
    public DateTimeOffset? BirthDate { get; set; }
    public GenderEnum? Gender { get; set; }
    public string Description { get; set; } = default!;
    public IEnumerable<CurrentUserMatchDto> Matches { get; set; } = default!;
}

