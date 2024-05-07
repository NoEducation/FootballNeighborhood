using System.ComponentModel.DataAnnotations.Schema;
using FootballNeighborhood.Domain.Entities.Common;
using FootballNeighborhood.Domain.Entities.Matches;
using FootballNeighborhood.Domain.Entities.Roles;
using FootballNeighborhood.Domain.Enums.Users;

namespace FootballNeighborhood.Domain.Entities.Users;

[Table("User")]
public class User : EntityWithAdditionalUserInfo
{
    public string Login { get; set; } = default!;
    public int RoleId { get; set; }
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public bool IsConfirmed { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public string? Description { get; set; }
    public virtual Role? Role { get; set; }
    public virtual ICollection<Match>? Matches { get; set; }
    public virtual ICollection<UserConfirmation>? UserConfirmations { get; set; }
}