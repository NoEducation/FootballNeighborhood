using FootballNeighborhood.Domain.Entities.Common;
using FootballNeighborhood.Domain.Entities.Permissions;
using FootballNeighborhood.Domain.Entities.Users;

namespace FootballNeighborhood.Domain.Entities.Roles;

public class Role : Entity<int>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public virtual ICollection<Permission>? Permissions { get; set; }
    public virtual ICollection<User>? Users { get; set; }
}